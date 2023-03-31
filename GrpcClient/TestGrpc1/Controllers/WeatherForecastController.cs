using Grpc.Net.Client;
using GrpcGreeterClient;
using Microsoft.AspNetCore.Mvc;

namespace TestGrpc1.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("testgrpc/{name}")]
        public async Task<IActionResult> TestGrpc([FromRoute] string name)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:7000");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                  new HelloRequestHieuNV { Name = name });
            return Ok("OKOKOK "+ reply.Message);
        }
    }
}