namespace Demo_APP.Controllers;

using Demo_APP.Dtos;
using Demo_APP.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class HighMobilityAuthController : ControllerBase
{
    private readonly IHighMobilityAuthService _highMobilityAuthService;
    private readonly IUpdateAppSettingsService updateAppSettingsService;
    private readonly IFirebaseConfigHelperService firebaseConfigHelperService;

    public HighMobilityAuthController(IHighMobilityAuthService highMobilityAuthService,
        IUpdateAppSettingsService updateAppSettingsService,
        IFirebaseConfigHelperService firebaseConfigHelperService)
    {
        _highMobilityAuthService = highMobilityAuthService;
        this.updateAppSettingsService = updateAppSettingsService;
        this.firebaseConfigHelperService = firebaseConfigHelperService;
    }

    [HttpGet("token")]
    public async Task<IActionResult> GetAccessToken()
    {
        try
        {
            var token = await _highMobilityAuthService.GetAccessTokenAsync();
            return Ok(new { accessToken = token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPut("updateDeviceRegistrationToken")]
    public async Task<IActionResult> UpdateDeviceRegistrationToken(string registrationToken)
    {
        try
        {
            await firebaseConfigHelperService.UpdateDeviceRegistrationTokenAsync(registrationToken);
            return Ok(registrationToken);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
