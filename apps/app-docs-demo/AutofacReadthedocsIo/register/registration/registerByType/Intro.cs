using Autofac;
using AutofacReadthedocsIo.register.registration.registrationConcepts;

namespace AutofacReadthedocsIo.register.registration.registerByType
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#register-by-type
    /// </summary>
    public class Intro
    {
        public Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType(typeof(ConfigReader));
            builder.RegisterType<MyComponent>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            IContainer container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                MyComponent component = scope.Resolve<MyComponent>();
            }
        }
    }
}
