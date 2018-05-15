﻿using System;

namespace Registration_Concepts
{
    internal class ConfigReader : IConfigReader
    {
        public ConfigReader(string sectionName)
        {
            Console.WriteLine("ConfigReader: " + sectionName);
        }
        public ConfigReader(string configSectionName, string sectionName)
        {
            Console.WriteLine("ConfigReader string configSectionName, string mysection: " + sectionName);
        }
        public void Read()
        {
            Console.WriteLine("Reading the config...");
        }
    }
}