namespace SpendingControlSystem.ViewModels
{
    public class IncomeRequestViewModel
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }
        public int UserId { get; set; }
    }
}
