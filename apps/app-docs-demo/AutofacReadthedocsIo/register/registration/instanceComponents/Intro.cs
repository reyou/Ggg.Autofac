using Autofac;
using System.IO;

namespace AutofacReadthedocsIo.register.registration.instanceComponents
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#instance-components
    /// </summary>
    public class Intro
    {
        public Intro()
        {
            ContainerBuilder builder = new ContainerBuilder();
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output).As<TextWriter>();
        }

        public void ExternallyOwned()
        {
            ContainerBuilder builder = new ContainerBuilder();
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output)
                .As<TextWriter>()
                .ExternallyOwned();

        }

        public void RegisterInstance()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(MySingleton.Instance).ExternallyOwned();

        }
    }
}
