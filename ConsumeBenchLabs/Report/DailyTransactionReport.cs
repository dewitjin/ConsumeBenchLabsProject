using ConsumeBenchLabs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeBenchLabs.Report
{
    public class DailyTransactionReport : IReport<TransactionPage>
    {
        public async Task<IList<TransactionPage>> GetPagesAsync()
        {
            var transactionPages = await Helper.BenchLabsAPI.GetTransactionPages();
            return transactionPages;
        }

        public List<Transaction> GetDailyTotal(IList<TransactionPage> pages)
        {
            var allTransactions = pages.SelectMany(x => x.Transactions)
              .GroupBy(x => x.Date) //return datetime, transaction groups
              .Select(x => new Transaction
              {
                  Date = x.Key,
                  Amount = x.Sum(x => x.Amount)
              })
              .OrderBy(x => x.Date)
              .ToList();

            return allTransactions;
        }
        public List<Transaction> GetDailyRunningTotal(List<Transaction> dailyTotals)
        {
         
            var runningTransactions = new List<Transaction>();
            var previousTotal = 0.0M;

            foreach (var daily in dailyTotals)
            {
                var runningAmount = daily.Amount + previousTotal;
                runningTransactions.Add(new Transaction() { Date = daily.Date, Amount = runningAmount });
                previousTotal = runningAmount;

            }

            return runningTransactions;
        }

        public async Task PrintDailyRunningTotalToConsole()
        {
            var transactionPages = await GetPagesAsync();
            var dailyTotals = GetDailyTotal(transactionPages);

            const int columnAlignment = -20;
            var runningTransactions = GetDailyRunningTotal(dailyTotals);

            foreach (var transaction in runningTransactions)
            {
                Console.WriteLine($"{transaction.Date,columnAlignment:d} {transaction.Amount,columnAlignment}");
            }
        }

        public async Task PrintDailyTotalToConsole()
        {
            const int columnAlignment = -20;
            var transactionPages = await GetPagesAsync();
            var allTransactions = GetDailyTotal(transactionPages);

            allTransactions.ForEach(x => Console.WriteLine($"{x.Date,columnAlignment:d} {x.Amount,columnAlignment}"));
        }
    }
}