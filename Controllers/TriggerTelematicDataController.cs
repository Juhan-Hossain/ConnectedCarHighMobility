#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
using Demo_APP.Dtos;
using Demo_APP.Interfaces;
using Demo_APP.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Demo_APP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TriggerTelematicDataController : ControllerBase
    {
       

        private readonly ILogger<TriggerTelematicDataController> _logger;
        private readonly ICallHighMobilityService _callHighMobilityService;
        private readonly IUpdateAppSettingsService _updatesAppSettingsService;

        public TriggerTelematicDataController(ILogger<TriggerTelematicDataController> logger,ICallHighMobilityService callHighMobilityService,
            IUpdateAppSettingsService updatesAppSettingsService)
        {
            _logger = logger;
            _callHighMobilityService = callHighMobilityService;
            _updatesAppSettingsService = updatesAppSettingsService;
            System.Net.ServicePointManager.SecurityProtocol =
             System.Net.SecurityProtocolType.Tls12 |
             System.Net.SecurityProtocolType.Tls13;
        }

        [HttpGet(Name = "GetNotifiedRealTimeData")]
        public IActionResult Get(string vin)
        {
            try
            {
                var vehicleData = _callHighMobilityService.CallAndProcessAsync(vin);
            }
            catch (Exception ex)
            {
                throw;
            }
            
            
           // var res = new List<WeatherForecast>();

            return Ok();
        }
    }
}
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.