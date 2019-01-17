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

            // Register types that expose interfaces...
            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            // Register instances of objects you create...
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output).As<TextWriter>();

            // Register expressions that execute to create objects...
            builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();

            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader reader = scope.Resolve<IConfigReader>();
            }
            Console.ReadLine();
        }


    }
}
