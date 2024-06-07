using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace RedisExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PubSubController : ControllerBase
    {
        readonly ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:1453");
        readonly RedisChannel redisChannel = new RedisChannel("mychannel", RedisChannel.PatternMode.Auto);

        [HttpGet]
        public async Task<IActionResult> SendMessage(string message)
        {
            await connectionMultiplexer.GetSubscriber().PublishAsync(redisChannel, message);
            return Ok();
        }
    }
}
