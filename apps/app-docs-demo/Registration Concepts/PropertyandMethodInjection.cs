using Autofac;
using System;

namespace Registration_Concepts
{
    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/register/prop-method-injection.html
    /// While constructor parameter injection is the preferred method of passing
    /// values to a component being constructed, you can also use property or
    /// method injection to provide values.
    /// Property injection uses writeable properties rather than constructor
    /// parameters to perform injection.Method injection sets dependencies
    /// by calling a method.
    /// </summary>
    internal class PropertyandMethodInjection
    {
        public static void PropertyInjection()
        {
            Console.WriteLine("\nPropertyandMethodInjection.PropertyInjection:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            builder.Register(c => new A { B = c.Resolve<B>() });
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                A aInstance = lifetimeScope.Resolve<A>();
                Console.WriteLine("aInstance.GetType(): " + aInstance.GetType());
            }
        }

        /// <summary>
        /// To support circular dependencies, use an activated event handler:
        /// </summary>
        public static void PropertyInjectionCirDep()
        {
            Console.WriteLine("\nPropertyandMethodInjection.PropertyInjectionCirDep:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            builder.Register(c => new A()).OnActivated(e => e.Instance.B = e.Context.Resolve<B>());
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                A aInstance = lifetimeScope.Resolve<A>();
                Console.WriteLine("aInstance.GetType(): " + aInstance.GetType());
            }
        }
    }
}