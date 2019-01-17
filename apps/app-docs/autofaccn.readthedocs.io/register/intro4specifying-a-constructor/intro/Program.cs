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
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            builder.RegisterType<MyComponent>().UsingConstructor(typeof(ILogger), typeof(IConfigReader));
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            IContainer container = builder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                MyComponent component = scope.Resolve<MyComponent>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }
}
