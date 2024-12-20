namespace SpendingControlSystem.Entities
{
    public class Budget:Entity<int>
    {
        public decimal MonthlyValue { get; set; }
        public DateTime YearMonth { get; set; }

        public virtual User User { get; set; }  
    }
}
