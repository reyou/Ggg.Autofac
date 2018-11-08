

using AutofacReadthedocsIo.testUtilitiesNs;

namespace AutofacReadthedocsIo.register.registration.registrationConcepts
{
    public class ConfigReader : IConfigReader
    {
        public ConfigReader(string mysection)
        {
            TestUtilities.WriteLine("ConfigReader initialized.");
            TestUtilities.WriteLine("mysection: " + mysection);
        }

        public void Read()
        {
            throw new System.NotImplementedException();
        }
    }
}