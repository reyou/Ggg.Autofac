using System;
using System.IO;
using Autofac;

namespace intro
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            // Create the builder with which components/services are registered.
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType(typeof(ConfigReader));
            IContainer container = builder.Build();
            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader reader = scope.Resolve<ConfigReader>();
            }
            Console.ReadLine();
        }


    }
}
