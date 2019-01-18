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

            builder.Register(c => new UserSession(DateTime.Now.AddMinutes(25)));


            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                UserSession userSession = scope.Resolve<UserSession>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal class UserSession
    {
        public UserSession(DateTime addMinutes)
        {
            Console.WriteLine("UserSession{{}} addMinutes: {0}", addMinutes.ToString(CultureInfo.InvariantCulture));
        }
    }
}
