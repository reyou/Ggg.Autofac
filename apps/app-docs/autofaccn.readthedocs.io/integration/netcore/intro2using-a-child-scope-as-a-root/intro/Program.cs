using Autofac;
using Autofac.Extensions.DependencyInjection;
using intro.IntroClasses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace intro
{
    public class Program
    {
        private const string RootLifetimeTag = "MyIsolatedRoot";
        public static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging();

            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<MessageHandler>().As<IHandler>();
            IContainer container = containerBuilder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope(RootLifetimeTag, b =>
            {
                b.Populate(serviceCollection, RootLifetimeTag);
            }))
            {
                // This service provider will have access to global singletons
                // and registrations but the "singletons" for things registered
                // in the service collection will be "rooted" under this
                // child scope, unavailable to the rest of the application.
                //
                // Obviously it's not super helpful being in this using block,
                // so likely you'll create the scope at app startup, keep it
                // around during the app lifetime, and dispose of it manually
                // yourself during app shutdown.
                AutofacServiceProvider serviceProvider = new AutofacServiceProvider(scope);
            }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
