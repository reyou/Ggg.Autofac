using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using intro.Controllers;
using intro.IntroClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace intro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();

            ContainerBuilder builder = new ContainerBuilder();

            // register your services against Autofac here
            builder.RegisterType<FooService>().As<IFooService>();

            // find and register all controllers from the current assembly
            // and autowire their properties
            builder.RegisterAssemblyTypes(typeof(ValuesController).GetTypeInfo().Assembly)
                .Where(
                    t =>
                        typeof(Controller).IsAssignableFrom(t) &&
                        t.Name.EndsWith("Controller", StringComparison.Ordinal)).PropertiesAutowired();

            builder.Populate(services);
            IContainer container = builder.Build();
            return container.Resolve<IServiceProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
