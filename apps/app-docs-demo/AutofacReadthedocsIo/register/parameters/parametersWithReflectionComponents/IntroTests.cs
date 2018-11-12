using Autofac;
using Autofac.Core;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.parameters.parametersWithReflectionComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/parameters.html#parameters-with-reflection-components
    /// </summary>
    [TestClass]
    public partial class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new ConfigReader("sectionName")).As<IConfigReader>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader configReader = scope.Resolve<IConfigReader>();
                TestUtilities.Attach(configReader);
            }
        }

        [TestMethod]
        public void IntroPassAParameterToAreflection()
        {
            ContainerBuilder builder = new ContainerBuilder();

            // Using a NAMED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter("configSectionName", "sectionName");

            // Using a TYPED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter(new TypedParameter(typeof(string), "sectionName"));

            // Using a RESOLVED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "configSectionName",
                        (pi, ctx) => "sectionName"));


            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader configReader = scope.Resolve<IConfigReader>();
                TestUtilities.Attach(configReader);
            }
        }
    }
}
