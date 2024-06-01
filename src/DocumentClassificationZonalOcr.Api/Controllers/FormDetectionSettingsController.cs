using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormDetectionSettingsController : ControllerBase
    {
        private readonly IFormDetectionSettingService _formDetectionSettingService;

        public FormDetectionSettingsController(IFormDetectionSettingService formDetectionSettingService)
        {
            _formDetectionSettingService = formDetectionSettingService;
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetSettings()
        {
            var result = await _formDetectionSettingService.GetSettingsAsync();
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateSettings(FormDetectionSettingDto settings)
        {
            var result = await _formDetectionSettingService.UpdateSettingsAsync(settings);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
