using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace pony.Controllers
{
    [ApiController]
    [Route("/api")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;

        public ApiController(ILogger<ApiController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(new[] {"hello", "world"});
        }
    }
}