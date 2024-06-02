using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<Result<FormDto>>> CreateFormAsync([FromBody] string name)
        {
            var result = await _formService.CreateFormAsync(name);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPut("modify/{formId}")]
        public async Task<ActionResult<Result<bool>>> ModifyFormNameAsync(int formId, [FromBody] string newName)
        {
            var result = await _formService.ModifyFormNameAsync(formId, newName);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("remove/{formId}")]
        public async Task<ActionResult<Result<bool>>> RemoveFormAsync(int formId)
        {
            var result = await _formService.RemoveFormAsync(formId);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/fields/add")]
        public async Task<ActionResult<Result<bool>>> AddFieldToFormAsync(int formId, [FromBody] FieldRequestDto field)
        {
            var result = await _formService.AddFieldToFormAsync(formId, field);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/samples/add")]
        public async Task<ActionResult<Result<bool>>> AddSampleToFormAsync(int formId, IFormFile file)
        {
            var result = await _formService.AddSampleToFormAsync(formId, file, null);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/fields")]
        public async Task<ActionResult<Result<CustomList<FieldDto>>>> GetAllFormFieldsAsync(int formId, [FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormFieldsAsync(formId, options);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/samples")]
        public async Task<ActionResult<Result<CustomList<FormSampleDto>>>> GetAllFormSamplesAsync(int formId, [FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormSamplesAsync(formId, options);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("get/{formId}")]
        public async Task<ActionResult<Result<FormDto>>> GetFormByIdAsync(int formId)
        {
            var result = await _formService.GetFormByIdAsync(formId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("all")]
        public async Task<ActionResult<Result<CustomList<FormDto>>>> GetAllFormsAsync([FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormsAsync(options);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/fields/create")]
        public async Task<ActionResult<Result<FieldDto>>> CreateFieldAsync(int formId, string name, FieldType type)
        {
            return await _formService.CreateFieldAsync(name, type, formId);
        }

        [HttpPost("{formId}/samples/create")]
        public async Task<ActionResult<Result<FormSampleDto>>> CreateFormSampleAsync(int formId, IFormFile image)
        {
            var result = await _formService.CreateFormSampleAsync(formId, image);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
