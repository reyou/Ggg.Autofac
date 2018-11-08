using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace AutofacReadthedocsIo.register.registration.registrationConcepts
{
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void IntroTest()
        {
            Intro intro = new Intro();
            Console.WriteLine(intro);
            if (Debugger.IsAttached) { Debugger.Break(); }
        }
    }
}
