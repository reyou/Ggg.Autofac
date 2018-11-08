namespace AutofacReadthedocsIo.register.registration.selectionOfAnImplementationByParameterValue
{
    public class StandardCard : CreditCard
    {
        public StandardCard(string accountId)
        {
            AccountId = accountId;
        }

        public string AccountId { get; set; }
    }
}