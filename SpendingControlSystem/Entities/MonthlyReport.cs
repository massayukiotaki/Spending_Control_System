namespace SpendingControlSystem.Entities
{
    public class MonthlyReport : Entity<int>
    {
        public DateTime YearMonth { get; set; }
        public decimal TotalRevenues{ get; set; }
        public decimal TotalCosts { get; set; }
        public decimal TotalInvestments { get; set; }

        public virtual User User { get; set; }
    }
}
