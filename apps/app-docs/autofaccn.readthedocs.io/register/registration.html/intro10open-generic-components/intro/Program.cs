using System;
using System.Globalization;
using System.IO;
using Autofac;

namespace intro
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            // Create the builder with which components/services are registered.
            ContainerBuilder builder = new ContainerBuilder();

            // Register an un-parameterised generic type, e.g. Repository&lt;&gt;.
            // Concrete types will be made as they are requested, e.g. with Resolve&lt;Repository&lt;int&gt;&gt;().
            // Configure the component so that every dependent component or call to Resolve()
            // within a single ILifetimeScope gets the same, shared instance. Dependent components in
            // different lifetime scopes will get different instances.
            builder.RegisterGeneric(typeof(NHibernateRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();


            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IRepository<User> repository = scope.Resolve<IRepository<User>>();
            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal class User
    {
    }

    internal interface IRepository<T>
    {
    }

    internal class NHibernateRepository<T> : IRepository<T>
    {
        public NHibernateRepository()
        {
            Console.WriteLine("NHibernateRepository{} created.");
        }
    }
}
