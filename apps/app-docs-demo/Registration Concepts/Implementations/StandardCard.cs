using System;

namespace Registration_Concepts
{
    internal class StandardCard : CreditCard
    {
        public StandardCard(string accountId)
        {
            Console.WriteLine("StandardCard constructor. accountId: " + accountId);
        }
    }
}