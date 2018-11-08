using Autofac;
using AutofacReadthedocsIo.register.registration.registerByType;
using AutofacReadthedocsIo.register.registration.registrationConcepts;
using AutofacReadthedocsIo.testUtilitiesNs;

namespace AutofacReadthedocsIo.register.registration.specifyingAconstructor
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#specifying-a-constructor
    /// </summary>
    public class Intro
    {
        public Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();
            builder.RegisterType<MyComponent>()
                .UsingConstructor(typeof(ILogger), typeof(IConfigReader));
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                MyComponent myComponent = scope.Resolve<MyComponent>();
                TestUtilities.WriteLine(myComponent);
            }
        }
    }
}
