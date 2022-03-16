using System;
using System.Threading.Tasks;
using System.Linq;
using ConsumeBenchLabs.Report;

namespace ConsumeBenchLabs
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter Y to show running amounts and hit enter.");

            var reply = Console.ReadLine();

            if (reply == "Y" || reply == "y")
            {
                DailyTransactionReport rpt = new DailyTransactionReport();
                await rpt.PrintDailyRunningTotalToConsole();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Good bye!");
            }
        }
    }
}



