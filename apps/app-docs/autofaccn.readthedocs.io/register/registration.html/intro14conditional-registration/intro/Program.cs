using System;
using System.Globalization;
using System.IO;
using Autofac;
using Autofac.Core;

namespace intro
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            // Only ServiceA will be registered.
            // Note the IfNotRegistered takes the SERVICE TYPE to
            // check for (the As<T>), NOT the COMPONENT TYPE
            // (the RegisterType<T>).
            builder.RegisterType<ServiceA>()
                .As<IService>();
            builder.RegisterType<ServiceB>()
                .As<IService>()
                .IfNotRegistered(typeof(IService));

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

            // Manager will be registered because both an IService
            // and HandlerB are registered. The OnlyIf predicate
            // can allow a lot more flexibility.
            builder.RegisterType<Manager>()
                .As<IManager>()
                .OnlyIf(reg =>
                    reg.IsRegistered(new TypedService(typeof(IService))) &&
                    reg.IsRegistered(new TypedService(typeof(HandlerB))));

            // This is when the conditionals actually run. Again,
            // they run in the order the registrations were added
            // to the ContainerBuilder.
            var container = builder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IManager resolve = scope.Resolve<IManager>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal interface IManager
    {
    }

    internal class Manager : IManager
    {
    }

    internal class HandlerC
    {
    }

    internal class HandlerB : IHandler
    {
    }

    internal interface IHandler
    {
    }

    internal class HandlerA : IHandler
    {
    }

    internal class ServiceB : IService
    {
    }

    internal interface IService
    {
    }

    internal class ServiceA : IService
    {
    }
}
