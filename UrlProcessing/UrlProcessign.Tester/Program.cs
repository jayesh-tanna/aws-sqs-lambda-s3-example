using System;
using System.Net.Http;
using UrlProcessing.Models;

namespace UrlProcessign.Tester
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:58010/")
            };

            var model = new UrlModel
            {
                Url = "https://www.google.com"
            };
            for (int i = 0; i < 1000; i++)
            {
                client.PostAsJsonAsync("api/Home", model);
                Console.WriteLine(i);
            }
            client.Dispose();
        }
    }
}
