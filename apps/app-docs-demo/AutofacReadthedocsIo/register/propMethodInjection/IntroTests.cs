using Autofac;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.propMethodInjection
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/prop-method-injection.html#property-injection
    /// </summary>
    [TestClass]
    public partial class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            builder.Register(c => new A { B = c.Resolve<B>() });

            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                A resolve = scope.Resolve<A>();
                TestUtilities.Attach(resolve);
            }
        }

        [TestMethod]
        public void IntroOnActivated()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            builder.Register(c => new A()).OnActivated(e => e.Instance.B = e.Context.Resolve<B>());

            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                A resolve = scope.Resolve<A>();
                TestUtilities.Attach(resolve);
            }
        }

        [TestMethod]
        public void IntroPropertiesAutowired()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            builder.RegisterType<A>().PropertiesAutowired();

            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                A resolve = scope.Resolve<A>();
                TestUtilities.Attach(resolve);
            }
        }

        [TestMethod]
        public void IntroWithProperty()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<B>();
            string propertyValue  = "QWER";
            builder.RegisterType<A>().WithProperty("PropertyName", propertyValue);
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                A resolve = scope.Resolve<A>();
                TestUtilities.Attach(resolve);
            }
        }
    }
}
