using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TraceTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public string Get()
        {
            _logger.LogError("Test on Node 3 with {@Headers}", _contextAccessor.HttpContext.Request.Headers);

            return "Dominik Node3";
        }
    }
}