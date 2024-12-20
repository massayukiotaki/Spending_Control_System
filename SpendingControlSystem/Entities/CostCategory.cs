namespace SpendingControlSystem.Entities
{
    public class CostCategory : Entity<int>
    {
        public string Name { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }

        public CostCategory()
        {
            Costs = new List<Cost>();
        }

    }
}
