using System;
using System.Globalization;
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

            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            // Specifies that the component being registered should
            // only be made the default for services
            // that have not already been registered.
            builder.RegisterType<FileLogger>().As<ILogger>().PreserveExistingDefaults();



            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {

            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal interface ILogger
    {
    }

    internal class FileLogger : ILogger
    {
    }

    internal class ConsoleLogger : ILogger
    {
    }


}
