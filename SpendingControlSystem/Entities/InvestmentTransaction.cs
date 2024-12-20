namespace SpendingControlSystem.Entities
{
    public class InvestmentTransaction : Entity<int>
    {
        public string Type { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public virtual Investment Investments { get; set; }
    }
}
