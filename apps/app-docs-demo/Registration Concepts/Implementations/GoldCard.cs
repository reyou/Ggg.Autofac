using System;

namespace Registration_Concepts
{
    internal class GoldCard : CreditCard
    {
        public GoldCard(string accountId)
        {
            Console.WriteLine("GoldCard constructor. accountId: " + accountId);
        }
    }
}