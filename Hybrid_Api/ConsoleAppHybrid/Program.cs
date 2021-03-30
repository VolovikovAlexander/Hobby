using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;


namespace ConsoleAppHybrid
{
    public class IPAddressLocation
    {
        public string IPAddress { get; set; }

        public string Location { get; set; }

        public DateTime CheckPeriod { get; set; }

        public override string ToString()
        {
            return $"IP -> {IPAddress} | location -> {Location} | сhanged -> {CheckPeriod}";
        }
    }

        class Program
    {
        static async Task GetIPInfo()
        {
            HttpClient client = new HttpClient();

            var url = "https://localhost:44321/api/Hybrid";
            var response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<IEnumerable< IPAddressLocation>>(responseBody);
            if (result != null)
            {
                foreach (var item in result)
                    Console.WriteLine(item.ToString());

                Console.WriteLine("==========================================");
            }
            else
                Console.WriteLine("Result empty");
        }

        static async Task SetIPAddress(string ipAddress)
        {
            Console.WriteLine($"Put IP {ipAddress} to host ..");
            HttpClient client = new HttpClient();

            var url = $"https://localhost:44321/api/Hybrid/GetInfo?ipAddress={ipAddress}";
            var response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IPAddressLocation>(responseBody);
            if(result != null)
            {
                Console.WriteLine($"  IP {ipAddress} ->  loсation {result.Location}");
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("============ Hyprid test API =============");
            // Что в начале
            GetIPInfo().Wait();
           
            // Сразу несколько скинем записей
            List<Task> _items = new List<Task>();
            for(int startPosition = 0; startPosition <= 254; startPosition++)
            {
                var _task = SetIPAddress($"134.19.{startPosition}.134");
                _items.Add(_task);
            }

            // Запускаем все в раз
            Task.WaitAny(_items.ToArray());

            // И покажем, что получилось
            GetIPInfo().Wait();


            Console.WriteLine("============ the end =============");
        }
    }
}
