using CaptureSolution.AutomaticReleaseApi.Controllers.Base;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class FieldController : AppControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("{fieldId}")]
        public async Task<IActionResult> GetFieldById(int fieldId)
        {
            var result = await _fieldService.GetFieldByIdAsync(fieldId);
            return CustomResult(result);
        }

        [HttpPut("{fieldId}")]
        public async Task<IActionResult> ModifyField(int fieldId, [FromBody] FieldRequestDto fieldRequest)
        {
            var result = await _fieldService.ModifyFieldAsync(fieldId, fieldRequest.Name,fieldRequest.Type);
            return CustomResult(result);
        }

        [HttpDelete("{fieldId}")]
        public async Task<IActionResult> RemoveField(int fieldId)
        {
            var result = await _fieldService.RemoveFieldAsync(fieldId);
            return CustomResult(result);
        }

    }
}
