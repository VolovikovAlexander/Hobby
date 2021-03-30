using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Hybrid.Core;

namespace Hybrid.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HybridController : ControllerBase
    {

        private readonly ILogger<HybridController> _logger;
        private HybridManager<IPAddressLocation> _manager = new HybridManager<IPAddressLocation>();

        public HybridController(ILogger<HybridController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Получить список IP адресов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<IPAddressLocation> Get()
        {
            return _manager.GetIPAddress().ToList();
        }

        /// <summary>
        /// Получить информацию о локации IP адреса. Если ее нет, то система сделает автоматический запрос 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetInfo")]
        public IPAddressLocation Get(string ipAddress)
        {
            return _manager.GetIPInfo(new IPAddressLocation() { IPAddress = ipAddress });
        }


        /// <summary>
        /// Получить информацию о локации IP адреса. Если ее нет, то система сделает автоматический запрос 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        [HttpPost]
        public IPAddressLocation Put(IPAddressLocation ipAddress)
        {
            return _manager.GetIPInfo(ipAddress);
        }
    }
}
