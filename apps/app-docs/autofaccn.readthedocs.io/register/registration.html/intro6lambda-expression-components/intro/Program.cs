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

            builder.RegisterType<B>();
            // Register a delegate as a component
            builder.Register(c => new A(c.Resolve<B>()));

            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                A reader = scope.Resolve<A>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal class B
    {
        public B()
        {
            Console.WriteLine("B{} created.");
        }
    }

    internal class A
    {
        public A(B resolve)
        {
            Console.WriteLine("A{} created.");
        }
    }
}
