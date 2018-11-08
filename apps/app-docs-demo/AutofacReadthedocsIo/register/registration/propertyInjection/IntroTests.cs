using Autofac;
using AutofacReadthedocsIo.register.registration.lambdaExpressionComponents;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.propertyInjection
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#property-injection
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new A() { MyB = c.ResolveOptional<B>() });
        }
    }
}
