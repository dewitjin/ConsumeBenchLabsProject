using System;

namespace ConsumeBenchLabs.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public string Ledger { get; set; }
        public decimal Amount { get; set; }
        public string Company { get; set; }
    }
}
