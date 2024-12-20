namespace SpendingControlSystem.ViewModels
{
    public class MonthlyReportRequestViewModel
    {
        public DateTime YearMonth { get; set; }
        public decimal TotalRevenues { get; set; }
        public decimal TotalCosts { get; set; }
        public decimal TotalInvestments { get; set; }
        public int UserId { get; set; }
    }
}
