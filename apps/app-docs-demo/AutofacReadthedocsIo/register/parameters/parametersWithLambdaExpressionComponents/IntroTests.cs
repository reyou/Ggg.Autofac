using Autofac;
using Autofac.Core;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.parameters.parametersWithLambdaExpressionComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/parameters.html#parameters-with-lambda-expression-components
    /// </summary>
    [TestClass]
    public partial class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            // Use TWO parameters to the registration delegate:
            // c = The current IComponentContext to dynamically resolve dependencies
            // p = An IEnumerable<Parameter> with the incoming parameter set
            builder.Register((c, p) =>
                    new ConfigReader(p.Named<string>("configSectionName")))
                .As<IConfigReader>();
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader configReader = scope.Resolve<IConfigReader>(new NamedParameter("configSectionName", "sectionName"));
                TestUtilities.Attach(configReader);
            }
        }
    }
}
