namespace SpendingControlSystem.ViewModels
{
    public class FinancialGoalRequestViewModel
    {
        public string Description { get; set; }
        public decimal ValueTarget { get; set; }
        public DateTime DateTarget { get; set; }
        public int UserId { get; set; }
    }
}
