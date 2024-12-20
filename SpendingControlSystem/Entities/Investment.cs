using Microsoft.Extensions.Hosting;

namespace SpendingControlSystem.Entities
{
    public class Investment : Entity<int>
    {
        public string Name{ get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }

        public virtual ICollection<InvestmentTransaction> InvestmentTransactions { get; set; }

        public Investment()
        {
            InvestmentTransactions = new List<InvestmentTransaction>();
        }

        public virtual User User { get; set; }
    }
}
