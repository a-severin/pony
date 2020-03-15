using System.Threading.Tasks;
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

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var result = await _storage.DeleteAsync(Request.Path.Value, Request.Body);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _storage.ReadAsync(Request.Path.Value);
            return Ok(result.ToString(Formatting.Indented));
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var result = await _storage.UpdateAsync(Request.Path.Value, Request.Body);
            if (result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> Put()
        {
            var result = await _storage.StoreAsync(Request.Path.Value, Request.Body);
            return Ok(result.ToString());
        }
    }
}