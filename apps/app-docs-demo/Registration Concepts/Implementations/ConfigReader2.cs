using System;

namespace Registration_Concepts
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