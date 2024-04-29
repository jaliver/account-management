namespace AccountService.Api.Models
{
    public class SavingsAccount
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
        public int CustomerId { get; set; }
    }
}
