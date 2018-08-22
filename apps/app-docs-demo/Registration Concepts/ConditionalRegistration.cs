using Autofac;
using Autofac.Core;
using RegistrationConcepts.Interfaces;
using RegistrationConcepts.Types;
using System;

namespace RegistrationConcepts
{
    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/register/registration.html#conditional-registration
    /// </summary>
    internal class ConditionalRegistration
    {
        public static void RunMain()
        {
            Console.WriteLine("\nConditionalRegistration.RunMain:\n");
            ContainerBuilder builder = new ContainerBuilder();
            // Only ServiceA will be registered.
            // Note the IfNotRegistered takes the SERVICE TYPE to
            // check for (the As<T>), NOT the COMPONENT TYPE
            // (the RegisterType<T>).
            builder.RegisterType<ServiceA>().As<IService>();
            builder.RegisterType<ServiceB>().As<IService>().IfNotRegistered(typeof(IService));
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IService isService = lifetimeScope.Resolve<IService>();
                Console.WriteLine("isService.GetType(): " + isService.GetType());

            }
        }

        public static void RunMain2()
        {
            Console.WriteLine("\nConditionalRegistration.RunMain2:\n");
            ContainerBuilder builder = new ContainerBuilder();
            // HandlerA WILL be registered - it's running
            // BEFORE HandlerB has a chance to be registered
            // so the IfNotRegistered check won't find it.
            //
            // HandlerC will NOT be registered because it
            // runs AFTER HandlerB. Note it can check for
            // the type "HandlerB" because HandlerB registered
            // AsSelf() not just As<IHandler>(). Again,
            // IfNotRegistered can only check for "As"
            // types.
            builder.RegisterType<HandlerA>()
                .AsSelf()
                .As<IHandler>()
                .IfNotRegistered(typeof(HandlerB));
            builder.RegisterType<HandlerB>()
                .AsSelf()
                .As<IHandler>();
            builder.RegisterType<HandlerC>()
                .AsSelf()
                .As<IHandler>()
                .IfNotRegistered(typeof(HandlerB));
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IHandler iHandler = lifetimeScope.Resolve<IHandler>();
                Console.WriteLine("iHandler.GetType(): " + iHandler.GetType());
            }
        }

        public static void RunMain3()
        {
            Console.WriteLine("\nConditionalRegistration.RunMain3:\n");
            // Manager will be registered because both an IService
            // and HandlerB are registered. The OnlyIf predicate
            // can allow a lot more flexibility.
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ServiceA>().As<IService>();
            builder.RegisterType<HandlerB>()
                .AsSelf()
                .As<IHandler>();
            builder.RegisterType<Manager>()
                .As<IManager>()
                .OnlyIf(reg =>
                    reg.IsRegistered(new TypedService(typeof(IService))) &&
                    reg.IsRegistered(new TypedService(typeof(HandlerB))));

            // This is when the conditionals actually run. Again,
            // they run in the order the registrations were added
            // to the ContainerBuilder.
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IManager manager = lifetimeScope.Resolve<IManager>();
                Console.WriteLine("manager.GetType(): " + manager.GetType());
            }
        }
    }
}