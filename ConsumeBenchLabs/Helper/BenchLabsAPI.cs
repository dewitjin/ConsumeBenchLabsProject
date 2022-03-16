using ConsumeBenchLabs.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ConsumeBenchLabs.Helper
{
    public static class BenchLabsAPI
    {
        public static HttpClient Initial()
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://resttest.bench.co/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static async Task<IList<TransactionPage>> GetTransactionPages()
        {
            var pages = new List<TransactionPage>();
            var client = Initial();
            var pageNumber = 1;
      
            try
            {
                while (pageNumber >= 1)
                {
                    var path = $"transactions/{pageNumber}.json"; //Note: don't include leading forward slash
                    var response = await client.GetAsync(path);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsAsync<TransactionPage>();//Requires the WebApi.Client package
                        pages.Add(result);
                        pageNumber = ++pageNumber;
                    }
                    else 
                    { 
                        break; 
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return pages;
        }
    }
}
