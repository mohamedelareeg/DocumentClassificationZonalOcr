using DocumentClassificationZonalOcr.Api.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
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
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{formSampleId}/zones")]
        public async Task<ActionResult<Result<IEnumerable<Zone>>>> GetAllZones(int formSampleId)
        {
            var result = await _formSampleService.GetAllZonesAsync(formSampleId);
            if (!result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formSampleId}/zones")]
        public async Task<ActionResult<Result<bool>>> AddZone(int formSampleId, [FromBody] ZoneDto zone)
        {
            var result = await _formSampleService.AddZoneAsync(formSampleId, zone);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }


        [HttpGet("{formSampleId}")]
        public async Task<ActionResult<Result<FormSample>>> GetFormSampleById(int formSampleId)
        {
            var result = await _formSampleService.GetFormSampleByIdAsync(formSampleId);
            if (!result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }
    }
}
