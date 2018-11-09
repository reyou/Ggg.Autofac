using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.defaultRegistrations
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#default-registrations
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            //If more than one component exposes the same service,
            //Autofac will use the last registered component as the
            //default provider of that service:
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<FileLogger>().As<ILogger>();
            // In this scenario, FileLogger will be the default
            // for ILogger because it was the last one registered.
            IContainer container = builder.Build();
            using (ILifetimeScope scope =container.BeginLifetimeScope())
            {
                ILogger resolve = scope.Resolve<ILogger>();
                TestUtilities.WriteLine(resolve);
                TestUtilities.Attach();
            }
        }

        [TestMethod]
        public void PreserveExistingDefaults()
        {
            //If more than one component exposes the same service,
            //Autofac will use the last registered component as the
            //default provider of that service:
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<FileLogger>().As<ILogger>().PreserveExistingDefaults();
            // In this scenario, FileLogger will be the default
            // for ILogger because it was the last one registered.
            // To override this behavior, use the PreserveExistingDefaults() modifier:
            IContainer container = builder.Build();
            using (ILifetimeScope scope =container.BeginLifetimeScope())
            {
                ILogger resolve = scope.Resolve<ILogger>();
                TestUtilities.WriteLine(resolve);
                TestUtilities.Attach();
            }
        }
    }
}
