using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.lambdaExpressionComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#lambda-expression-components
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Register()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new A(c.Resolve<B>()));
        }
    }
}
