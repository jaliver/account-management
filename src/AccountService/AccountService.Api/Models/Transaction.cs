namespace AccountService.Api.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SavingsAccountId { get; set; }
        public SavingsAccount SavingsAccount { get; set; } = default!;
    }
}
