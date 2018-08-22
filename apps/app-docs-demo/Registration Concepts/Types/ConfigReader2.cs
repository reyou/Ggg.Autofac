using RegistrationConcepts.Interfaces;
using System;

namespace RegistrationConcepts.Types
{
    internal class ConfigReader2 : IConfigReader
    {
        public ConfigReader2(string configSectionName)
        {
            Console.WriteLine("ConfigReader2 configSectionName: " + configSectionName);
        }

        public void Read()
        {
            throw new NotImplementedException();
        }
    }
}