using Autofac;
using DemoApp.Types;
using System;

namespace DemoApp
{

    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/getting-started/index.html#structuring-the-application
    /// </summary>
    class Program
    {
        /// <summary>
        /// The container itself is a lifetime scope, and you can technically
        /// just resolve things right from the container. It is not
        /// recommended to resolve from the container directly, however.
        /// </summary>
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            CreateBuilder();
            CreateBuilder2();
            Console.WriteLine("Main method reach to end. Press a key to continue...");
            Console.ReadLine();
        }
        private static void CreateBuilder()
        {
            // Create your builder.
            ContainerBuilder builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<SomeType>().As<IService>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<SomeType>().AsSelf().As<IService>();

        }
        private static void CreateBuilder2()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            // The WriteDate method is where we'll make use
            // of our dependency injection. We'll define that
            // in a bit.
            WriteDate();

        }

        private static void WriteDate()
        {
            // Create the scope, resolve your IDateWriter,
            // use it, then dispose of the scope.
            using (ILifetimeScope scope = Container.BeginLifetimeScope())
            {
                IDateWriter writer = scope.Resolve<IDateWriter>();
                writer.WriteDate();
            }
        }


    }
}
