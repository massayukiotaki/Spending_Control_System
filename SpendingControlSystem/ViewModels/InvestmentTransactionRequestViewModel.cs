namespace SpendingControlSystem.ViewModels
{
    public class InvestmentTransactionRequestViewModel
    {
        public string Type { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int InvestmentId { get; set; }
    }
}
