﻿using Autofac;
using Autofac.Extensions.DependencyInjection;
using intro.IntroClasses;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace intro
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // register services with DI.
            ServiceCollection serviceCollection = new ServiceCollection();

            // The Microsoft.Extensions.Logging package provides this one-liner
            // to add logging services.
            serviceCollection.AddLogging();

            ContainerBuilder containerBuilder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(serviceCollection);

            // Make your Autofac registrations. Order is important!
            // If you make them BEFORE you call Populate, then the
            // registrations in the ServiceCollection will override Autofac
            // registrations; if you make them AFTER Populate, the Autofac
            // registrations will override. You can make registrations
            // before or after Populate, however you choose.
            containerBuilder.RegisterType<MessageHandler>().As<IHandler>();


            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            IContainer container = containerBuilder.Build();
            AutofacServiceProvider serviceProvider = new AutofacServiceProvider(container);

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
