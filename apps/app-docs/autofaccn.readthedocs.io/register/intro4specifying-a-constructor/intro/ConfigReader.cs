using System;

namespace intro
{
    internal class ConfigReader : IConfigReader
    {
        public ConfigReader()
        {
            Console.WriteLine("ConfigReader initialized.");
        }
        public ConfigReader(string mysection)
        {
            Console.WriteLine("ConfigReader initialized. mysection: {0}", mysection);
        }
    }
}