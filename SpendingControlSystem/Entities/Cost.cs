namespace SpendingControlSystem.Entities
{
    public class Cost : Entity<int>
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public virtual User User { get; set; }
        public virtual CostCategory CostCategory { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
