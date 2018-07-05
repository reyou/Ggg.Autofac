using Autofac;
using RegisterAssemblyTypesConsole.Interfaces;
using System;
using System.Reflection;

namespace RegisterAssemblyTypesConsole
{
    class Program
    {
        private static readonly ContainerBuilder Builder = new ContainerBuilder();
        static void Main(string[] args)
        {
            // RegisterAssemblyTypes();
            RegisterAssemblyTypes2();
            RunAllTasks();
            Console.WriteLine("\nMain method reach to end. Press a key to continue...");
            Console.ReadLine();
        }

        private static void RunAllTasks()
        {
            IContainer container = Builder.Build();
            using (ILifetimeScope lifetimeScope = container.BeginLifetimeScope())
            {
                IDataRepo dataRepo = lifetimeScope.Resolve<IDataRepo>();
                ILogger logger = lifetimeScope.Resolve<ILogger>();
                IMessenger messenger = lifetimeScope.Resolve<IMessenger>();
                dataRepo.Create();
                logger.Log();
                messenger.SendMessage();

            }
        }

        private static void RegisterAssemblyTypes()
        {
            Assembly dataAccess = Assembly.GetExecutingAssembly();
            Builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
        }

        private static void RegisterAssemblyTypes2()
        {

            Assembly dataAccess = Assembly.GetExecutingAssembly();
            Builder.RegisterAssemblyTypes(dataAccess).AsImplementedInterfaces();
        }

    }
}
