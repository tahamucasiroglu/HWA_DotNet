using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MemoryCacheExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMemoryCache _memoryCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            string cacheKey = "Tarih";

            if (!_memoryCache.TryGetValue(cacheKey, out DateTime dateTime))
            {
                dateTime = DateTime.Now;
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
                    SlidingExpiration = TimeSpan.FromMinutes(2)
                };
                _memoryCache.Set(cacheKey, dateTime, cacheEntryOptions);
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                ResponseDateReal = DateTime.Now,
                ResponseDateCache = dateTime,
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
