using System.Collections.Generic;


namespace ConsumeBenchLabs.Models
{
    public class TransactionPage
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}
