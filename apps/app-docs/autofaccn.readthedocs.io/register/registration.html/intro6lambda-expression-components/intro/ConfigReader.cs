using System;

namespace intro
{
    internal class ConfigReader : IConfigReader
    {
        public ConfigReader(string mysection)
        {
            Console.WriteLine("ConfigReader initialized. mysection: {0}", mysection);
        }
    }
}