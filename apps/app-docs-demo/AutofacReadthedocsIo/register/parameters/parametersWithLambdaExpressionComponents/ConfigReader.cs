using System.Collections;
using System.Collections.Generic;
using AutofacReadthedocsIo.testUtilitiesNs;

namespace AutofacReadthedocsIo.register.parameters.parametersWithLambdaExpressionComponents
{
    public class ConfigReader : IConfigReader
    {
        public ConfigReader(string configSectionName)
        {
            TestUtilities.Attach();
        }
        public ConfigReader(List<string> configSectionName)
        {
           TestUtilities.Attach();
        }
    }
}