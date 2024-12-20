using SpendingControlSystem.Entities;

namespace SpendingControlSystem.ViewModels
{
    public class BudgetRequestViewModel
    {
        public decimal MonthlyValue { get; set; }
        public DateTime YearMonth { get; set; }
        public int UserId { get; set; }
    }
}
