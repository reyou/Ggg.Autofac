using System;
using Autofac;

namespace intro
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create your builder.
            ContainerBuilder builder = new ContainerBuilder();

            // Usually you're only interested in exposing the type
            // via its interface:
            builder.RegisterType<SomeType>().As<IService>();

            // However, if you want BOTH services (not as common)
            // you can say so:
            builder.RegisterType<SomeType>().AsSelf().As<IService>();
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
