namespace CustomerService.Api.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? SavingsAccountId { get; set; }
    }
}
