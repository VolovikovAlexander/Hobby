using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.IO;
using API.Core;


namespace FileInfoConsole
{

    /// <summary>
    /// Проверка работы API на примере загруженного файла
    /// </summary>

    class Program
    {
        public static HttpClient _client;
        
        /// <summary>
        /// Проверить работу API - сбросить пример 
        /// </summary>
        /// <returns></returns>
        static async Task SetSimpleInfoToServer(ApiFileInfo _model)
        {
            if (_model == null)
                return;

            _client = new HttpClient();
            string _url = $"http://localhost:64474/FileInfo/api";

            var modelString = JsonConvert.SerializeObject(_model);
            var item = new StringContent(modelString, Encoding.UTF8, "application/json");

            try
            {
                var response = await _client.PostAsync(_url, item);
                var responseBody = response.Content.ReadAsStringAsync().Result;

                // Получаем данные
                var resultObject = JsonConvert.DeserializeObject<ApiTableInfo>(responseBody);
                if (string.IsNullOrEmpty(resultObject.ErrorText))
                {
                    Console.WriteLine("\nПолучен следующий результат: " + Environment.NewLine);
                    Console.WriteLine(resultObject.ToString());
                }
                else
                    Console.WriteLine($"От сервера получена ошибка -> {resultObject.ErrorText}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при подключении и выполнении операций с сервером!");
                Console.WriteLine(ex.Message);
            }
        }

     

        static void Main(string[] args)
        {
            // Готовим структуру - верно
            var _model = new ApiFileInfo
            {
                Name = "FirstTestFile.csv",
                Body = @"Наименование,ИНН,КПП
                    ООО ОптТорг,7736624353,770301001
                    ООО Славянка,7713716199,771301001",
                type = DelimetrType.Comma
            };

            SetSimpleInfoToServer(_model).Wait();

            // Готовим структуру - НЕ верно
            _model = new ApiFileInfo
            {
                Name = "FirstTestFile.csv",
                Body = @"Наименование,ИНН,КПП
                    ООО ОптТорг,7736624353,770301001
                    ООО Славянка,7713716199,771301001",
                type = DelimetrType.Tab
            };

            SetSimpleInfoToServer(_model).Wait();


            // Готовим структуру - верно
            _model = new ApiFileInfo
            {
                Name = "FirstTestFile.csv",
                Body = @"Наименование" + "\t" + "ИНН" + "\t" + @"КПП
                    ООО ОптТорг" + "\t" + "7736624353" + "\t" + "770301001",
                type = DelimetrType.Tab
            };

            SetSimpleInfoToServer(_model).Wait();
    }
    }
}
