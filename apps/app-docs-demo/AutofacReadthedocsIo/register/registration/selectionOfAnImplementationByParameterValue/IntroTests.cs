using Autofac;
using AutofacReadthedocsIo.testUtilitiesNs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutofacReadthedocsIo.register.registration.selectionOfAnImplementationByParameterValue
{
    /// <summary>
    /// https://autofac.readthedocs.io/en/latest/register/registration.html#selection-of-an-implementation-by-parameter-value
    /// </summary>
    [TestClass]
    public class IntroTests
    {
        [TestMethod]
        public void Intro()
        {
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
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                CreditCard card = scope.Resolve<CreditCard>(new NamedParameter("accountId", "12345"));
                TestUtilities.WriteLine(card);
                TestUtilities.Attach();
            }

        }
    }
}
