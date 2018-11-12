namespace AutofacReadthedocsIo.register.parameters.parametersWithReflectionComponents
{
    public partial class IntroTests
    {
        public class ConfigReader : IConfigReader
        {
            public ConfigReader(string configSectionName)
            {
                // Store config section name
            }

            // ...read configuration based on the section name.
        }
    }
}
