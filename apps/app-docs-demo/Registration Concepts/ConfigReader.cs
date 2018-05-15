using System;

namespace Registration_Concepts
{
    internal class ConfigReader : IConfigReader
    {
        public ConfigReader(string mysection)
        {
            Console.WriteLine("ConfigReader: " + mysection);
        }

        public void Read()
        {
            Console.WriteLine("Reading the config...");
        }
    }
}