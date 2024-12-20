namespace SpendingControlSystem.Entities
{
    public class PaymentType : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }

        public PaymentType()
        {
            Costs = new List<Cost>();
        }

    }
}
