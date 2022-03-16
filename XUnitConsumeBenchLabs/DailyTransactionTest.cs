using ConsumeBenchLabs.Models;
using ConsumeBenchLabs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitConsumeBenchLabs
{
    public class DailyTransactionTest
    {
        [Fact]
        public async void GetPagesAsync_Should_Return_List_of_TransactionPages()
        {
            //Arrange
            var expected = await ConsumeBenchLabs.Helper.BenchLabsAPI.GetTransactionPages();//method is called in GetPagesAsync

            //Act
            var rpt = new DailyTransactionReport();
            var actual = await rpt.GetPagesAsync();

            //Assert
            Assert.IsType<List<TransactionPage>>(actual);
            Assert.True(actual.Any());
            Assert.Equal(expected.Count, actual.Count);
        }

        [Fact]
        public void GetDailyTotal_Should_Return_List_of_Daily_Transactions()
        {
            //Arrange   
            var dateTime = DateTime.Now; //make date for both transactions the same
            var transactionOneExpected = new Transaction()
            {
                Date = dateTime,
                Ledger = "Phone & Internet Expense",
                Amount = 100,
                Company = "SHAW CABLESYSTEMS CALGARY AB"
            };

            var transactionTwoExpected = new Transaction()
            {
                Date = dateTime,
                Ledger = "Phone & Internet Expense",
                Amount = 300,
                Company = "SHAW CABLESYSTEMS CALGARY AB"
            }; //With same date as the previous transaction

            var transactionsExpected = new List<Transaction>
            {
                transactionOneExpected,
                transactionTwoExpected
            };

            var transactionPageExpected = new TransactionPage()
            {
                TotalCount = 2,
                Page = 1,
                Transactions = transactionsExpected
            };

            var pagesExpected = new List<TransactionPage>
            {
                transactionPageExpected
            };

            var rpt = new DailyTransactionReport();

            //Act
            var listActual = rpt.GetDailyTotal(pagesExpected);

            //Assert
            Assert.Single(listActual); //Grouping dates together means list should contain only one item for this test
            Assert.IsType<List<Transaction>>(listActual);

            var sumOfTransactionExpected = transactionOneExpected.Amount + transactionTwoExpected.Amount;
            Assert.Equal(sumOfTransactionExpected, listActual.First().Amount);
        }

        [Fact]
        public void GetDailyRunningTotal_Should_Return_List_of_Running_Transactions()
        {
            //Arrange   
            var dateTimeOne = DateTime.Now; 
            var dateTimeTwo = dateTimeOne.AddDays(1);
            var dateTimeThree = dateTimeTwo.AddDays(1);

            var transactionOneExpected = new Transaction()
            {
                Date = dateTimeOne,
                Ledger = "Phone & Internet Expense",
                Amount = 100,
                Company = "SHAW CABLESYSTEMS CALGARY AB"
            };

            var transactionTwoExpected = new Transaction()
            {
                Date = dateTimeTwo,
                Ledger = "Phone & Internet Expense",
                Amount = 300,
                Company = "SHAW CABLESYSTEMS CALGARY AB"
            };

            var transactionThreeExpected = new Transaction()
            {
                Date = dateTimeThree,
                Ledger = "Phone & Internet Expense",
                Amount = -400,
                Company = "SHAW CABLESYSTEMS CALGARY AB"
            };

            var transactionsExpected = new List<Transaction>
            {
                transactionOneExpected,
                transactionTwoExpected,
                transactionThreeExpected
            };

            var transactionPageExpected = new TransactionPage()
            {
                TotalCount = 3,
                Page = 1,
                Transactions = transactionsExpected
            };

            var pagesExpected = new List<TransactionPage>
            {
                transactionPageExpected
            };

            var rpt = new DailyTransactionReport();

            //Act
            var listDailyExpected = rpt.GetDailyTotal(pagesExpected); 
            var listRunningActual = rpt.GetDailyRunningTotal(listDailyExpected);

            //Assert
            Assert.Equal(3, listRunningActual.Count); //Should contain three objects in list, three different transaction
            Assert.IsType<List<Transaction>>(listRunningActual);

            var sum = transactionOneExpected.Amount;
            Assert.Equal(sum, listRunningActual.First().Amount);

            sum = transactionOneExpected.Amount + transactionTwoExpected.Amount;
            Assert.Equal(sum, listRunningActual.Single(x => x.Date == dateTimeTwo).Amount);

            sum = transactionOneExpected.Amount + transactionTwoExpected.Amount + transactionThreeExpected.Amount;
            Assert.Equal(sum, listRunningActual.Single(x => x.Date == dateTimeThree).Amount);
        }
    }
}
