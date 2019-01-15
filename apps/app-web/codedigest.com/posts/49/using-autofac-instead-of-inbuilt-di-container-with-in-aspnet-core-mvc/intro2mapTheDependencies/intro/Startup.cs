using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using intro.IntroClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<DBModel>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //Now register our services with Autofac container
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterModule(new RepositoryHandlerModule());
            builder.Populate(services);
            IContainer container = builder.Build();
            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(container);



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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }

    public class DBModel : DbContext
    {
        public DBModel(DbContextOptions<DBModel> dbContextOptions) : base(dbContextOptions)
        {

        }
    }

    public class SiteAnalyticsServices
    {
    }

    public class PostRepository : IPostRepository
    {
    }

    public interface IPostRepository
    {
    }
}
