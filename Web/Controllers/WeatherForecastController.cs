using ApplicationCore.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IComputadoraService _computadoraService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IComputadoraService computadoraService)
        {
            _logger = logger;
            _computadoraService = computadoraService;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            return await _computadoraService.SayHi();
        }
    }
}