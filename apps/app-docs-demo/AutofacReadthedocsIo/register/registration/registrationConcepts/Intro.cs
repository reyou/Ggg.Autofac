using Autofac;
using System.IO;

namespace AutofacReadthedocsIo.register.registration.registrationConcepts
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#registration-concepts
    /// </summary>
    public class Intro
    {
        public Intro()
        {
            // Create the builder with which components/services are registered.
            ContainerBuilder builder = new ContainerBuilder();
            // Register types that expose interfaces...
            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            // Register instances of objects you create...
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output).As<TextWriter>();

            // Register expressions that execute to create objects...
            builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();

            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader reader = scope.Resolve<IConfigReader>();
            }

        }
    }
}
