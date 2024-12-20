namespace SpendingControlSystem.Entities
{
    public class Income : Entity<int>
    {
        public decimal Value { get; set; }
        public string Description { get; set; }
        public DateTime PaymentDate { get; set; }

        public virtual User User { get; set; }
    }
}
