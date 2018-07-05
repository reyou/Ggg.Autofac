using Autofac;
using Autofac.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Registration_Concepts;
using System;
using System.IO;

namespace RegistrationConcepts
{
    /// <summary>
    /// http://autofac.readthedocs.io/en/latest/register/registration.html
    /// You register components with Autofac by creating a ContainerBuilder
    /// and informing the builder which components expose which services.
    /// </summary>

    [TestClass]
    public class Program
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
            PropertyInjection();
            SelectionofanImplementation();
            OpenGenericComponents();
            ComponentWithAnyNumberOfServices();
            AsSelfSample();
            DefaultRegistrations();
            ConditionalRegistration.RunMain();
            ConditionalRegistration.RunMain2();
            ConditionalRegistration.RunMain3();
            ReflectionComponentRegistration();
            ParameterswithLambdaExpression();
            PropertyandMethodInjection.PropertyInjection();
            PropertyandMethodInjection.PropertyInjectionCirDep();
            PropertyandMethodInjection.PropertyReflectionComponent();
            PropertyandMethodInjection.PropertyAndValueToWireUp();
            PropertyandMethodInjection.MethodInjection();
            PropertyandMethodInjection.ActivatingEventHandler();
            AssemblyScanning.ScanningForTypes();
            AssemblyScanning.FilteringTypes();
            Console.WriteLine("\nMain method reach to end. Press a key to continue...");
            Console.ReadLine();
        }

        private static void ParameterswithLambdaExpression()
        {
            Console.WriteLine("\nParameterswithLambdaExpression:\n");
            ContainerBuilder builder = new ContainerBuilder();
            // Use TWO parameters to the registration delegate:
            // c = The current IComponentContext to dynamically resolve dependencies
            // p = An IEnumerable<Parameter> with the incoming parameter set
            builder.Register((c, p) => new ConfigReader2(p.Named<string>("configSectionName"))).As<IConfigReader>();
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IConfigReader configReader = lifetimeScope.Resolve<IConfigReader>(new NamedParameter("configSectionName", "ASection"));
                Console.WriteLine("configReader.GetType(): " + configReader.GetType());
            }

        }

        [TestMethod]
        public void ParameterswithLambdaExpressionTest()
        {
            ParameterswithLambdaExpression();
        }


        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/parameters.html
        /// </summary>
        private static void ReflectionComponentRegistration()
        {
            Console.WriteLine("\nReflectionComponentRegistration:\n");
            ContainerBuilder builder = new ContainerBuilder();
            // Using a NAMED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter("configSectionName", "sectionName");

            // Using a TYPED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter(new TypedParameter(typeof(string), "sectionName"));

            // Using a RESOLVED parameter:
            builder.RegisterType<ConfigReader>()
                .As<IConfigReader>()
                .WithParameter(
                    new ResolvedParameter(
                        (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "configSectionName",
                        (pi, ctx) => "sectionName"));

            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IConfigReader configReader = lifetimeScope.Resolve<IConfigReader>(new NamedParameter("sectionName", "ASection"));
                Console.WriteLine("configReader.GetType(): " + configReader.GetType());

            }
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#default-registrations
        /// If more than one component exposes the same service, Autofac will use
        /// the last registered component as the default provider of that service:
        /// </summary>
        private static void DefaultRegistrations()
        {
            Console.WriteLine("\nDefaultRegistrations:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<ConsoleLogger>().As<ILogger>();
            builder.RegisterType<FileLogger>().As<ILogger>();
            // To override this behavior, use the PreserveExistingDefaults() modifier:
            builder.RegisterType<FallbackLogger>().As<ILogger>().PreserveExistingDefaults();
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                ILogger logger = lifetimeScope.Resolve<ILogger>();
                Console.WriteLine("logger.GetType(): " + logger.GetType());

            }
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#services-vs-components
        /// If you want to expose a component as a set of services as well as
        /// using the default service, use the AsSelf method:
        /// </summary>
        private static void AsSelfSample()
        {
            Console.WriteLine("\nAsSelfSample:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>()
                .AsSelf()
                .As<ILogger>()
                .As<ICallInterceptor>();
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                ILogger logger = lifetimeScope.Resolve<ILogger>();
                Console.WriteLine("ILogger resolved");
                ICallInterceptor icaCallInterceptor = lifetimeScope.Resolve<ICallInterceptor>();
                Console.WriteLine("ICallInterceptor resolved");
                CallLogger callLogger = lifetimeScope.Resolve<CallLogger>();
                Console.WriteLine("CallLogger resolved");
            }

        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#services-vs-components
        /// </summary>
        private static void ComponentWithAnyNumberOfServices()
        {
            Console.WriteLine("\nComponentWithAnyNumberOfServices:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterType<CallLogger>()
                .As<ILogger>()
                .As<ICallInterceptor>();
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                ILogger logger = lifetimeScope.Resolve<ILogger>();
                Console.WriteLine("ILogger resolved");
                ICallInterceptor icaCallInterceptor = lifetimeScope.Resolve<ICallInterceptor>();
                Console.WriteLine("ICallInterceptor resolved");
            }

        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#open-generic-components
        /// Autofac supports open generic types. Use the RegisterGeneric() builder method
        /// </summary>
        private static void OpenGenericComponents()
        {
            /*builder.RegisterGeneric(typeof(NHibernateRepository<>))
       .As(typeof(IRepository<>))
       .InstancePerLifetimeScope();*/
            /*// Autofac will return an NHibernateRepository<Task>
            var tasks = container.Resolve<IRepository<Task>>();
            */
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#register-by-type
        /// A cleaner, type-safe syntax can be achieved if a delegate to create
        /// CreditCard instances is declared and a delegate factory is used.
        /// </summary>
        private static void SelectionofanImplementation()
        {
            Console.WriteLine("\nSelectionofanImplementation:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register<CreditCard>(
                (c, p) =>
                {
                    string accountId = p.Named<string>("accountId");
                    if (accountId.StartsWith("9"))
                    {
                        return new GoldCard(accountId);
                    }
                    return new StandardCard(accountId);
                });
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                CreditCard card = container.Resolve<CreditCard>(new NamedParameter("accountId", "12345"));
                Console.WriteLine("CreditCard resolved");

            }
        }

        /// <summary>
        /// http://autofac.readthedocs.io/en/latest/register/registration.html#property-injection
        /// http://autofac.readthedocs.io/en/latest/register/prop-method-injection.html
        /// Property injection is not recommended in the majority of cases.
        /// Alternatives like the Null Object pattern, overloaded constructors or
        /// constructor parameter default values make it possible to create cleaner,
        /// “immutable” components with optional dependencies using constructor injection.
        /// </summary>
        private static void PropertyInjection()
        {
            Console.WriteLine("\nPropertyInjection:\n");
            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(c => new A { MyB = c.ResolveOptional<B>() });
            IContainer container = builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                A userSession = lifetimeScope.Resolve<A>();
            }
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
