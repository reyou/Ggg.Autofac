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
            StringWriter output = new StringWriter();
            // Configure the component so that instances
            // are never disposed by the container.
            builder.RegisterInstance(output).As<TextWriter>().ExternallyOwned();
            builder.RegisterInstance(MySingleton.Instance).As<ISingleton>().ExternallyOwned();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                TextWriter component = scope.Resolve<TextWriter>();
                ISingleton mySingleton = scope.Resolve<ISingleton>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal interface ISingleton
    {
    }

    internal class MySingleton : ISingleton
    {
        public static object Instance => new MySingleton();
    }
}
