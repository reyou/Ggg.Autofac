using Autofac;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.servicesVsComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#services-vs-components
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                CallLogger callLogger = scope.Resolve<CallLogger>();
                TestUtilities.WriteLine(callLogger);
                TestUtilities.Attach();
            }
        }

        [TestMethod]
        public void RegisterType()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>()
                .As<ILogger>()
                .As<ICallInterceptor>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                ILogger iLogger = scope.Resolve<ILogger>();
                ICallInterceptor iCallInterceptor = scope.Resolve<ICallInterceptor>();
                TestUtilities.WriteLine(iLogger);
                TestUtilities.WriteLine(iCallInterceptor);
                TestUtilities.Attach();
            }
        }

        [TestMethod]
        public void AsSelf()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>()
                .AsSelf()
                .As<ILogger>()
                .As<ICallInterceptor>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                // These will all work because we exposed
                // the appropriate services in the registration:
                ILogger iLogger = scope.Resolve<ILogger>();
                ICallInterceptor callInterceptor = scope.Resolve<ICallInterceptor>();
                CallLogger callLogger = scope.Resolve<CallLogger>();
                TestUtilities.WriteLine(iLogger);
                TestUtilities.WriteLine(callInterceptor);
                TestUtilities.WriteLine(callLogger);
                TestUtilities.Attach();
            }
        }

        [TestMethod]
        public void RegisterTypeThrowsException()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>()
                .As<ILogger>()
                .As<ICallInterceptor>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                // This WON'T WORK anymore because we specified
                // service overrides on the component:
                CallLogger callLogger = scope.Resolve<CallLogger>();
                TestUtilities.WriteLine(callLogger);
                TestUtilities.Attach();
            }
        }

        [TestMethod]
        public void IntroThrowsException()
        {
            ContainerBuilder builder = new ContainerBuilder();
            // This exposes the service "CallLogger"
            builder.RegisterType<CallLogger>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                ILogger callLogger = scope.Resolve<ILogger>();
                TestUtilities.WriteLine(callLogger);
                TestUtilities.Attach();
            }
        }
    }
}
