using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZipInfo.Core;

namespace ZipInfo.Controllers
{
    [ApiController]
    [Route("ZipInfo")]
    public class ApiController : ControllerBase
    {
        private ZipInfoManager _manager;
        public ApiController()
        {
        }

        [HttpGet]
        public Core.ZipInfo Get(int zipCode)
        {
            _manager = new ZipInfoManager(zipCode);
            _manager.Run().Wait();

            return _manager.Info;
        }
    }
}
