using AutoMapper;
using FluentValidation;
using Generator.API.Middleware;
using Generator.Application.DTOs;
using Generator.Application.Handlers;
using Generator.Application.Interfaces;
using Generator.Application.Mapping;
using Generator.Application.Models;
using Generator.Application.Persistence;
using Generator.Application.Validations;
using Generator.Infrastructure.IO;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddTransient<IValidator<ChoiceDto>, ChoiceDtoValidator>();
            services.AddTransient<IValidator<ReceivedName>, TableNameValidator>();
            services.AddSingleton<IWriter, FileWriter>();

            services.AddDbContext<GeneratorContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMediatR(typeof(ChoiceHandler));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
