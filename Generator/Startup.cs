//using AutoMapper;
//using FluentValidation;
//using FluentValidation.AspNetCore;
//using Generator.Application.DTOs;
//using Generator.Models.Validations;
//using MediatR;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;

//namespace Generator.Application
//{
//    public class Startup
//    {
//        public Startup(IConfiguration configuration)
//        {
//            this.Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//            services.AddMvc()
//                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
//                .AddFluentValidation();
//            services.AddTransient<IValidator<PayloadDto>, PayloadValidator>();

//            services.AddAutoMapper(typeof(Startup));
//            services.AddMediatR(typeof(Startup));
//            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                .AddCookie();
//        }

//        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
//        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseDeveloperExceptionPage();
//            }
//            else
//            {
//                app.UseHsts(); // important xd
//            }

//            app.UseAuthentication();
//            app.UseMvc();
//        }
//    }
//}
