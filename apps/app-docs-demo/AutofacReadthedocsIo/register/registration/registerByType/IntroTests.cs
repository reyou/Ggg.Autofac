﻿using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.registerByType
{
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void IntroTest()
        {
            Intro intro = new Intro();
            TestUtilities.WriteLine(intro);
            TestUtilities.Attach();
        }
    }
}
