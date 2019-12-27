using AutoMapper;
using FluentValidation;
using Generator.API.Middleware;
using Generator.Application.Commands;
using Generator.Application.Dtos;
using Generator.Application.Handlers;
using Generator.Application.Interfaces;
using Generator.Application.Mapping;
using Generator.Application.Persistence;
using Generator.Application.Queries;
using Generator.Application.Validations;
using Generator.Infrastructure.Configuration;
using Generator.Infrastructure.IO;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Generator.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddOptions();
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration["Authorization:Authority"];
                });
            services.Configure<PicturesMessageBusDtoWriterConfiguration>(Configuration.GetSection("FileWriterConfiguration"));

            services.AddScoped<IValidator<SaveChosenPicturesCommand>, SaveChosenPicturesCommandValidator>();
            services.AddScoped<IValidator<GetRandomPicturesQuery>, GetRandomPicturesQueryValidator>();
            services.AddScoped<IWriter<PicturesMessageBusDto>, PicturesMessageBusDtoWriter>();

            services.AddDbContext<GeneratorContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(typeof(SaveChosenPicturesCommandHandler));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGeneratorExceptionMiddleware();
            }
            else
            {
                app.UseGeneratorExceptionMiddleware();
                app.UseHsts(); // important
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseAuthentication();
        }
    }
}
