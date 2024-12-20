using Microsoft.EntityFrameworkCore;
using SpendingControlSystem.Data.Map;
using SpendingControlSystem.Entities;

namespace SpendingControlSystem.Data
{
    public class SpendingControlSystemDBContext : DbContext
    {
        public SpendingControlSystemDBContext(DbContextOptions<SpendingControlSystemDBContext>options)
            : base(options)
            {   
            }
        public DbSet<User> Users { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<CostCategory> CostCategories { get; set; }
        public DbSet<FinancialGoal> FinancialGoals { get; set; }
        public DbSet<Income> Incomes { get; set; }  
        public DbSet<Investment> Investments { get; set; }
        public DbSet<InvestmentTransaction> InvestmentTransactions { get; set; }
        public DbSet<MonthlyReport> MonthlyReports { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new BudgetMap());
            modelBuilder.ApplyConfiguration(new CostMap());
            modelBuilder.ApplyConfiguration(new CostCategoryMap());
            modelBuilder.ApplyConfiguration(new FinancialGoalMap());
            modelBuilder.ApplyConfiguration(new IncomeMap());
            modelBuilder.ApplyConfiguration(new InvestmentMap());
            modelBuilder.ApplyConfiguration(new InvestmentTransactionMap());
            modelBuilder.ApplyConfiguration(new MonthlyReportMap());
            modelBuilder.ApplyConfiguration(new PaymentTypeMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
