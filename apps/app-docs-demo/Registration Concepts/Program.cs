using Autofac;
using System;
using System.IO;

namespace Registration_Concepts
{
    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/register/registration.html
    /// You register components with Autofac by creating a ContainerBuilder
    /// and informing the builder which components expose which services.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Register();
            RegisterbyType();
            RegisterComponents();
            SpecifyingaConstructor();
            InstanceComponents();
            RegisteringSingleton();
            LambdaExpressionComponents();
            ComplexParameters();
            Console.WriteLine("\nMain method reach to end. Press a key to continue...");
            Console.ReadLine();
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#complex-parameters
        /// Constructor parameters can’t always be declared with simple constant values.
        /// Rather than puzzling over how to construct a value of a certain type using
        /// an XML configuration syntax, use code:
        /// </summary>
        private static void ComplexParameters()
        {
            Console.WriteLine("\nComplexParameters:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new UserSession(DateTime.Now.AddMinutes(25)));
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                UserSession userSession = lifetimeScope.Resolve<UserSession>();
            }
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#lambda-expression-components
        /// </summary>
        private static void LambdaExpressionComponents()
        {
            Console.WriteLine("\nLambdaExpressionComponents:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new A(c.Resolve<B>()));

        }

        /// <summary>
        /// Rather than tying those components directly to the singleton,
        /// it can be registered with the container as an instance:
        /// </summary>
        private static void RegisteringSingleton()
        {
            Console.WriteLine("\nRegisteringSingleton:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterInstance(MySingleton.Instance).ExternallyOwned();
            IContainer container = builder.Build();
            MySingleton mySingleton = container.Resolve<MySingleton>();
            Console.WriteLine("MySingleton resolved.");
        }

        /// <summary>
        /// Something to consider when you do this is that Autofac automatically
        /// handles disposal of registered components and you may want to
        /// control the lifetime yourself rather than having Autofac call
        /// Dispose on your object for you. In that case, you need to
        /// register the instance with the ExternallyOwned method:
        /// </summary>
        private static void InstanceComponents()
        {
            Console.WriteLine("\nInstanceComponents:\n");
            ContainerBuilder builder = new ContainerBuilder();
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output).As<TextWriter>().ExternallyOwned();
            IContainer container = builder.Build();
            TextWriter textWriter = container.Resolve<TextWriter>();
            Console.WriteLine("TextWriter resolved.");
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#specifying-a-constructor
        /// </summary>
        private static void SpecifyingaConstructor()
        {
            Console.WriteLine("\nSpecifyingaConstructor:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();
            builder.RegisterType<MyComponent>()
                .UsingConstructor(typeof(ILogger), typeof(IConfigReader));
            IContainer container = builder.Build();
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                MyComponent component = scope.Resolve<MyComponent>();
            }
        }

        /// <summary>
        /// When you resolve your component, Autofac will see that you
        /// have an ILogger registered, but you don’t have an IConfigReader
        /// registered. In that case, the second constructor will be chosen
        /// since that’s the one with the most parameters that can be found in the container.
        /// </summary>
        private static void RegisterComponents()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<MyComponent>();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            IContainer container = builder.Build();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                MyComponent component = scope.Resolve<MyComponent>();
            }
        }

        /// <summary>
        /// Components generated by reflection are typically registered by type:
        /// </summary>
        private static void RegisterbyType()
        {
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>();
            builder.RegisterType(typeof(ConfigReader));

        }

        private static void Register()
        {
            // Create the builder with which components/services are registered.
            ContainerBuilder builder = new ContainerBuilder();

            // Register types that expose interfaces...
            builder.RegisterType<ConsoleLogger>().As<ILogger>();

            // Register instances of objects you create...
            StringWriter output = new StringWriter();
            builder.RegisterInstance(output).As<TextWriter>();

            // Register expressions that execute to create objects...
            builder.Register(c => new ConfigReader("mysection")).As<IConfigReader>();

            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IConfigReader reader = scope.Resolve<IConfigReader>();
                reader.Read();
            }
        }
    }
}
