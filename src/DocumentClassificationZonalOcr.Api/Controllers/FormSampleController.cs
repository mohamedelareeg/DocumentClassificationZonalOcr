using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormSampleController : ControllerBase
    {
        private readonly IFormSampleService _formSampleService;

        public FormSampleController(IFormSampleService formSampleService)
        {
            _formSampleService = formSampleService;
        }


        [HttpPut("{formSampleId}")]
        public async Task<ActionResult<Result<bool>>> ModifyFormSampleImage(int formSampleId, [FromForm] IFormFile newImage)
        {
            var result = await _formSampleService.ModifyFormSampleImageAsync(formSampleId, newImage);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{formSampleId}/zones")]
        public async Task<ActionResult<Result<IEnumerable<ZoneDto>>>> GetAllZones(int formSampleId)
        {
            var result = await _formSampleService.GetAllZonesAsync(formSampleId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formSampleId}/zones")]
        public async Task<ActionResult<Result<bool>>> AddZone(int formSampleId, [FromBody] ZoneRequestDto zone)
        {
            var result = await _formSampleService.AddZoneAsync(formSampleId, zone);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }


        [HttpGet("{formSampleId}")]
        public async Task<ActionResult<Result<FormSampleDto>>> GetFormSampleById(int formSampleId)
        {
            var result = await _formSampleService.GetFormSampleByIdAsync(formSampleId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{formSampleId}")]
        public async Task<ActionResult<Result<bool>>> RemoveFormSample(int formSampleId)
        {
            var result = await _formSampleService.RemoveFormSampleAsync(formSampleId);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
