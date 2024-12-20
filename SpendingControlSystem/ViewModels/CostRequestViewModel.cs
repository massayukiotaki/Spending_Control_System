namespace SpendingControlSystem.ViewModels
{
    public class CostRequestViewModel
    {
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int CostCategoryId { get; set; }
        public int PaymentTypeId { get; set; }  
    }
}
