using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Core;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]/api")]
    public class FileInfoController : ControllerBase
    {
        ApiTableInfo _tableInfo = null;

        /// <summary>
        /// Передача файла POSTом в виде структуры
        /// </summary>
        /// <param name="localFile">Структура с файлом</param>
        /// <param name="localType">Разделитель (число)</param>
        /// <returns></returns>
        [HttpPost]
        public ApiTableInfo Set(ApiFileInfo localFile)
        {
            if (localFile != null)
            {
                _tableInfo = new ApiTableInfo(localFile);
                return _tableInfo;
            }

            return null;
        }

        #region "Статика - помошники"

        public static string ReadAsString(IFormFile file)
        {
            var result = new StringBuilder();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    result.AppendLine(reader.ReadLine());
            }

            return result.ToString();
        }

        #endregion

        /// <summary>
        /// Загрузка данных через WEB страницу
        /// </summary>
        /// <param name="formFile"></param>
        /// <param name="charDelimiter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("loadFile")]
        public ApiTableInfo Set(List<IFormFile> formFile, int charDelimiter )
        {
            if (formFile != null && charDelimiter > 0)
            {
                var filePath = Path.GetTempFileName();
                var localFile = new ApiFileInfo()
                { 
                    Name = filePath,
                    type = (DelimetrType)charDelimiter,
                    Body = ReadAsString(formFile.FirstOrDefault())
                };

                _tableInfo = new ApiTableInfo(localFile);
                return _tableInfo;
            }

            return new ApiTableInfo() { ErrorText = "Не корректно переданы параметры!" };
        }



        /// <summary>
        /// Просто заглушка для GET
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IApiTableInfo Get()
        {
            return new ApiTableInfo();
        }
    }
}
