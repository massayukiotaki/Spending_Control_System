namespace SpendingControlSystem.Entities
{
    public class FinancialGoal : Entity<int>
    {
        public string Description { get; set; }
        public decimal ValueTarget { get; set; }
        public DateTime DateTarget { get; set; }

        public virtual User User { get; set; }
    }
}
