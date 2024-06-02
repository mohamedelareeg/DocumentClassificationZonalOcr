using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("{fieldId}")]
        public async Task<ActionResult<Result<FieldDto>>> GetFieldById(int fieldId)
        {
            var result = await _fieldService.GetFieldByIdAsync(fieldId);
            if (!result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }



        [HttpPut("{fieldId}")]
        public async Task<ActionResult<Result<bool>>> ModifyField(int fieldId, [FromBody] FieldRequestDto fieldRequest)
        {
            var result = await _fieldService.ModifyFieldNameAsync(fieldId, fieldRequest.Name);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{fieldId}")]
        public async Task<ActionResult<Result<bool>>> RemoveField(int fieldId)
        {
            var result = await _fieldService.RemoveFieldAsync(fieldId);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

    }
}
