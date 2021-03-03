using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace TraceTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _contextAccessor;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            _logger.LogError("Test on Node 2 with {@Headers}", _contextAccessor.HttpContext.Request.Headers);

            var client = _httpClientFactory.CreateClient();
            var result = await client.GetAsync("http://localhost:5020/WeatherForecast?test=three");

            var content = await result.Content.ReadAsStringAsync();

            return $"Result from node: {content}";
        }
    }
}