using CaptureSolution.AutomaticReleaseApi.Controllers.Base;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class FormSampleController : AppControllerBase
    {
        private readonly IFormSampleService _formSampleService;

        public FormSampleController(IFormSampleService formSampleService)
        {
            _formSampleService = formSampleService;
        }

        [HttpPut("{formSampleId}")]
        public async Task<IActionResult> ModifyFormSampleImage(int formSampleId, [FromForm] IFormFile newImage)
        {
            var result = await _formSampleService.ModifyFormSampleImageAsync(formSampleId, newImage);
            return CustomResult(result);
        }

        [HttpGet("{formSampleId}/zones")]
        public async Task<IActionResult> GetAllZones(int formSampleId)
        {
            var result = await _formSampleService.GetAllZonesAsync(formSampleId);
            return CustomResult(result);
        }

        [HttpPost("{formSampleId}/zones")]
        public async Task<IActionResult> AddZone(int formSampleId, [FromBody] ZoneRequestDto zone)
        {
            var result = await _formSampleService.AddZoneAsync(formSampleId, zone);
            return CustomResult(result);
        }
        [HttpDelete("{formSampleId}/zones")]
        public async Task<IActionResult> RemoveAllZones(int formSampleId)
        {
            var result = await _formSampleService.RemoveAllZonesAsync(formSampleId);
            return CustomResult(result);
        }

        [HttpGet("{formSampleId}")]
        public async Task<IActionResult> GetFormSampleById(int formSampleId)
        {
            var result = await _formSampleService.GetFormSampleByIdAsync(formSampleId);
            return CustomResult(result);
        }

        [HttpDelete("{formSampleId}")]
        public async Task<IActionResult> RemoveFormSample(int formSampleId)
        {
            var result = await _formSampleService.RemoveFormSampleAsync(formSampleId);
            return CustomResult(result);
        }
    }
}
