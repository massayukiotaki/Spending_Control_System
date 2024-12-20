namespace SpendingControlSystem.Entities
{
    public class User : Entity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Budget> Budgets { get; set; }
        public virtual ICollection<Cost> Costs { get; set; }
        public virtual ICollection<FinancialGoal> FinancialGoals { get; set; }
        public virtual ICollection<Income> Incomes { get; set; }
        public virtual ICollection<Investment> Investments { get; set; }
        public virtual ICollection<MonthlyReport> MonthlyReports { get; set; }

        public User()
        {
            Budgets = new List<Budget>(); 
            Costs = new List<Cost>(); 
            FinancialGoals = new List<FinancialGoal>(); 
            Incomes = new List<Income>(); 
            Investments = new List<Investment>(); 
            MonthlyReports = new List<MonthlyReport>(); 
        }
    }         
}
