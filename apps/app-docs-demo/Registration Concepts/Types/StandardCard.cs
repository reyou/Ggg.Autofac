using System;

namespace RegistrationConcepts.Types
{
    internal class StandardCard : CreditCard
    {
        public StandardCard(string accountId)
        {
            Console.WriteLine("StandardCard constructor. accountId: " + accountId);
        }
    }
}