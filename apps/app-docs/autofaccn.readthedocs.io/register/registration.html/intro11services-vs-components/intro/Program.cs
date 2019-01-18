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

            // This exposes the service "CallLogger"
            builder.RegisterType<CallLogger>()
                .As<ILogger>()
                .As<ICallInterceptor>();


            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                // These will both work because we exposed
                // the appropriate services in the registration:
                scope.Resolve<ILogger>();
                scope.Resolve<ICallInterceptor>();

                // This WON'T WORK anymore because we specified
                // service overrides on the component:
                string wantException = "false";
                if (bool.Parse(wantException))
                {
                    scope.Resolve<CallLogger>();
                }
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal interface ICallInterceptor
    {
    }

    internal interface ILogger
    {
    }

    internal class CallLogger : ILogger, ICallInterceptor
    {
    }
}
