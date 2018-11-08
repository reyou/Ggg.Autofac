using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AutofacReadthedocsIo.register.registration.complexParameters
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#complex-parameters
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new UserSession(DateTime.Now.AddMinutes(25)));

        }
    }
}
