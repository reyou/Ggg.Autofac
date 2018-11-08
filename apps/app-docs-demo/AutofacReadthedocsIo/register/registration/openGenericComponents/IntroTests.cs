using Autofac;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.openGenericComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#open-generic-components
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(NHibernateRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IRepository<Computer> repository = scope.Resolve<IRepository<Computer>>();
                TestUtilities.WriteLine(repository);
                TestUtilities.Attach();
            }
        }
    }
}
