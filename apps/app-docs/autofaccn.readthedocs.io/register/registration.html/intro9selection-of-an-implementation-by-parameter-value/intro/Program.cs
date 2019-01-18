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


            builder.Register<CreditCard>(
                (c, p) =>
                {
                    string accountId = p.Named<string>("accountId");
                    if (accountId.StartsWith("9"))
                    {
                        return new GoldCard(accountId);
                    }
                    else
                    {
                        return new StandardCard(accountId);
                    }
                });

            // Build the container to finalize registrations
            // and prepare for object resolution.
            IContainer container = builder.Build();

            // Now you can resolve services using Autofac. For example,
            // this line will execute the lambda expression registered
            // to the IConfigReader service.
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                var card = container.Resolve<CreditCard>(new NamedParameter("accountId", "12345"));

            }
            Console.WriteLine("program finished.");
            Console.ReadLine();
        }


    }

    internal class StandardCard : CreditCard
    {
        public StandardCard(string accountId)
        {
            Console.WriteLine("StandardCard{} created.");
        }
    }

    internal class GoldCard : CreditCard
    {
        public GoldCard(string accountId)
        {
            Console.WriteLine("GoldCard{} created.");
        }
    }

    internal class CreditCard
    {
        public CreditCard()
        {
            Console.WriteLine("CreditCard{} created.");
        }
    }
}
