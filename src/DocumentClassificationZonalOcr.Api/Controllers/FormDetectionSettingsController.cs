using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CaptureSolution.AutomaticReleaseApi.Controllers.Base;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class FormDetectionSettingsController : AppControllerBase
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
            return CustomResult(result);
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateSettings(FormDetectionSettingDto settings)
        {
            var result = await _formDetectionSettingService.UpdateSettingsAsync(settings);
            return CustomResult(result);
        }
    }
}
