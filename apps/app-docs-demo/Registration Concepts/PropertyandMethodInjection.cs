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

        /// <summary>
        /// If the component is a reflection component, use the
        /// PropertiesAutowired() modifier to inject properties:
        /// </summary>
        public static void PropertyReflectionComponent()
        {
            Console.WriteLine("\nPropertyandMethodInjection.PropertyReflectionComponent:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<A>().PropertiesAutowired();
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                A aInstance = lifetimeScope.Resolve<A>();
                Console.WriteLine("aInstance.GetType(): " + aInstance.GetType());
            }
        }

        /// <summary>
        /// If you have one specific property and value to wire up,
        /// you can use the WithProperty() modifier:
        /// </summary>
        public static void PropertyAndValueToWireUp()
        {
            Console.WriteLine("\nPropertyandMethodInjection.PropertyAndValueToWireUp:\n");
            ContainerBuilder builder = new ContainerBuilder();
            int propertyValue = 96;
            builder.RegisterType<A>().WithProperty("PropertyName", propertyValue);
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                A aInstance = lifetimeScope.Resolve<A>();
                Console.WriteLine("aInstance.GetType(): " + aInstance.GetType());
                Console.WriteLine("aInstance.PropertyName: " + aInstance.PropertyName);
            }
        }

        /// <summary>
        /// The simplest way to call a method to set a value on a component
        /// is to use a lambda expression component and handle the method
        /// call right in the activator:
        /// </summary>
        public static void MethodInjection()
        {
            Console.WriteLine("\nPropertyandMethodInjection.MethodInjection:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TheDependency>();
            builder.Register(c =>
            {
                MyObjectType myObjectType = new MyObjectType();
                TheDependency dep = c.Resolve<TheDependency>();
                myObjectType.SetTheDependency(dep);
                return myObjectType;
            });
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                MyObjectType instance = lifetimeScope.Resolve<MyObjectType>();
                Console.WriteLine("instance.GetType(): " + instance.GetType());
            }
        }

        /// <summary>
        /// If you can’t use a registration lambda, you can add an activating event handler:
        /// </summary>
        public static void ActivatingEventHandler()
        {
            Console.WriteLine("\nPropertyandMethodInjection.ActivatingEventHandler:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<TheDependency>();
            builder
                .RegisterType<MyObjectType>()
                .OnActivating(e =>
                {
                    TheDependency dep = e.Context.Resolve<TheDependency>();
                    e.Instance.SetTheDependency(dep);
                });
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                MyObjectType instance = lifetimeScope.Resolve<MyObjectType>();
                Console.WriteLine("instance.GetType(): " + instance.GetType());
            }
        }
    }
}