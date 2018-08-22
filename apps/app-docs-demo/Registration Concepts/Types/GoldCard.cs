using System;

namespace RegistrationConcepts.Types
{
    internal class GoldCard : CreditCard
    {
        public GoldCard(string accountId)
        {
            Console.WriteLine("GoldCard constructor. accountId: " + accountId);
        }
    }
}