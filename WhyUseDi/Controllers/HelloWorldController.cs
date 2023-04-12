using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhyUseDI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly ILogger<HelloWorldController> _logger;
        private readonly GetHelloWorldQuery _service;

        public HelloWorldController(ILogger<HelloWorldController> logger, GetHelloWorldQuery service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public string Get()
        {
            return _service.Get();
        }
    }
}