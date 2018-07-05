using Autofac;
using System;
using System.Reflection;

namespace RegistrationConcepts
{
    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/register/scanning.html
    /// Autofac can use conventions to find and register components
    /// in assemblies. You can scan and register individual types or you
    /// can scan specifically for Autofac modules.
    /// </summary>
    internal class AssemblyScanning
    {
        /// <summary>
        /// Otherwise known as convention-driven registration or scanning,
        /// Autofac can register a set of types from an assembly according
        /// to user-specified rules:
        /// </summary>
        public static void ScanningForTypes()
        {
            Console.WriteLine("\nAssemblyScanning.ScanningForTypes:\n");
            Assembly dataAccess = Assembly.GetExecutingAssembly();
            ContainerBuilder builder = new ContainerBuilder();
            /*Each RegisterAssemblyTypes() call will apply one set of rules
             only - multiple invocations of RegisterAssemblyTypes() are 
             necessary if there are multiple different sets of components to register.*/
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            Console.WriteLine("AssemblyScanning.ScanningForTypes completed.");

        }

        public static void FilteringTypes()
        {
            throw new NotImplementedException();
        }
    }
}