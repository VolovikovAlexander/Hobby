using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace ZipCodeTesting
{
    public class AnswerObject
    {
        public string weather { get; set; }

        public string timeZone { get; set; }

        public string cityName { get; set; }

        public string coordinate { get; set; }
    }

    class Program
    {
        /// <summary>
        /// Нагрузочное тестирирование
        /// </summary>
        /// <returns></returns>
        static async Task ZipCodeTesting()
        {
            HttpClient client = new HttpClient();

            // Arkansas(AR)  Little Rock
            for (int startZip = 72201; startZip <= 72217; startZip++)
            {
                var url = "https://localhost:44324/ZipInfo?zipCode={0}";
                url = string.Format(url, startZip);
                var response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();

                AnswerObject result = new AnswerObject();
                result = JsonConvert.DeserializeObject<AnswerObject>(responseBody);
                Console.WriteLine($"{result.cityName} - {startZip}({result.timeZone}) -> {result.weather}, {result.coordinate}");
            }


            // California (CA)	Sacramento Los Angeles Beverly Hills
            for (int startZip = 94204; startZip <= 94209; startZip++)
            {
                var url = "https://localhost:44324/ZipInfo?zipCode={0}";
                url = string.Format(url, startZip);
                var response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();

                AnswerObject result = new AnswerObject();
                result = JsonConvert.DeserializeObject<AnswerObject>(responseBody);
                Console.WriteLine($"{result.cityName} - {startZip}({result.timeZone}) -> {result.weather}, {result.coordinate}");
            }
        }

        static void Main(string[] args)
        {
            ZipCodeTesting().Wait();
        }
    }
}
