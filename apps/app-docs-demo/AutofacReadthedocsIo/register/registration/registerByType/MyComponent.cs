using AutofacReadthedocsIo.register.registration.registrationConcepts;
using AutofacReadthedocsIo.testUtilitiesNs;

namespace AutofacReadthedocsIo.register.registration.registerByType
{
    public class MyComponent
    {
        public MyComponent()
        {
            TestUtilities.Attach();
        }

        public MyComponent(ILogger logger)
        {
            TestUtilities.Attach();
        }

        public MyComponent(ILogger logger, IConfigReader reader)
        {
            TestUtilities.Attach();
        }
    }
}