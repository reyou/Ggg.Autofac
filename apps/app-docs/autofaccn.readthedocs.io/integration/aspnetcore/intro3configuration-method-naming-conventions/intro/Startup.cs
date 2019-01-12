/*
 * https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#configuration-method-naming-conventions
 */

using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace intro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Do Startup-ish things like read configuration.
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This is the default if you don't have an environment specific method.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This only gets called if your environment is Development. The
        // default ConfigureServices won't be automatically called if this
        // one is called.
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // Add things to the service collection that are only for the
            // development environment.
        }

        // This is the default if you don't have an environment specific method.
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add things to the Autofac ContainerBuilder.
        }

        // This only gets called if your environment is Production. The
        // default ConfigureContainer won't be automatically called if this
        // one is called.
        public void ConfigureProductionContainer(ContainerBuilder builder)
        {
            // Add things to the ContainerBuilder that are only for the
            // production environment.
        }

        // This only gets called if your environment is Staging. The
        // default Configure won't be automatically called if this one is called.
        public void ConfigureStaging(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            // Set up the application for staging.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // This is the default if you don't have an environment specific method.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Set up the application.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
