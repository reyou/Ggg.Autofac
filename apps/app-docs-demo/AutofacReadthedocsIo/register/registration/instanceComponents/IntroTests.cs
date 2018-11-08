using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.instanceComponents
{
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Intro intro = new Intro();
            TestUtilities.WriteLine(intro);
        }

        [TestMethod]
        public void ExternallyOwned()
        {
            Intro intro = new Intro();
            intro.ExternallyOwned();
            TestUtilities.WriteLine(intro);
        }

        [TestMethod]
        public void RegisterInstance()
        {
            Intro intro = new Intro();
            intro.RegisterInstance();
            TestUtilities.WriteLine(intro);
        }
    }
}
