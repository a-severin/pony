using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using pony.Storage;

namespace pony.Controllers
{
    [ApiController]
    [Route("{path}")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IPonyStorage _storage;

        public ApiController(ILogger<ApiController> logger, IPonyStorage storage)
        {
            _logger = logger;
            _storage = storage;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _storage.ReadAsync(Request.Path.Value);
            return Ok(result.ToString(Formatting.Indented));
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            await _storage.StoreAsync(Request.Path.Value, Request.Body);
            return Ok("data saved");
        }
    }
}