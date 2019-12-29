using System;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Generator.Identity.Models.Settings;
using Generator.Identity.Persistence;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Generator.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;

            Env = env;
        }

        public IWebHostEnvironment Env { get; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Settings
            var settings = new AppSettings();
            Configuration.GetSection("AppSettings").Bind(settings);
            services.AddSingleton(settings);

            var builder = services.AddIdentityServer(
                options => options.PublicOrigin = settings.Site.PublicOrigin.ToString());
            Console.WriteLine(settings.Site.PublicOrigin.ToString());
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddEntityFrameworkSqlServer();

            if (Env.IsDevelopment())
            {
                builder.AddInMemoryIdentityResources(DevConfig.GetIdentityResources())
                    .AddInMemoryApiResources(DevConfig.GetApis())
                    .AddInMemoryClients(DevConfig.GetClients())
                    .AddDeveloperSigningCredential();
            }
            else
            {
                builder.AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql =>
                            sql.MigrationsAssembly(migrationsAssembly));
                }).AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sql =>
                            sql.MigrationsAssembly(migrationsAssembly));
                    options.EnableTokenCleanup = true;
                });
                var cert = new X509Certificate2(
                    Configuration["Key:Cert"],
                    Configuration["Key:Password"]);
                builder.AddSigningCredential(cert);
                services.AddDbContext<KeysContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), o =>
                    {
                        o.MigrationsAssembly(migrationsAssembly);
                    });
                });
                services.AddDataProtection().PersistKeysToDbContext<KeysContext>();
            }

            services.AddDbContext<UserContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"), options =>
                        options.MigrationsAssembly(migrationsAssembly)));

            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SaveTokens = true;
                    options.ClientId = settings.Google.ClientId;
                    options.ClientSecret = settings.Google.ClientSecret;
                });

            // Antiforgery
            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "anti-forgery";
                options.HeaderName = "XSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseIdentityServer();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapDefaultControllerRoute(); });
        }
    }
}
