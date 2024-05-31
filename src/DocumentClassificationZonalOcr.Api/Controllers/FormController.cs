using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Result<Form>>> CreateFormAsync([FromBody] string name)
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
        public async Task<ActionResult<Result<bool>>> AddFieldToFormAsync(int formId, [FromBody] Field field)
        {
            var result = await _formService.AddFieldToFormAsync(formId, field);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/samples/add")]
        public async Task<ActionResult<Result<bool>>> AddSampleToFormAsync(int formId, [FromBody] FormSample sample)
        {
            var result = await _formService.AddSampleToFormAsync(formId, sample);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{formId}/fields")]
        public async Task<ActionResult<Result<IEnumerable<Field>>>> GetAllFormFieldsAsync(int formId)
        {
            var result = await _formService.GetAllFormFieldsAsync(formId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{formId}/samples")]
        public async Task<ActionResult<Result<IEnumerable<FormSample>>>> GetAllFormSamplesAsync(int formId)
        {
            var result = await _formService.GetAllFormSamplesAsync(formId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("get/{formId}")]
        public async Task<ActionResult<Result<Form>>> GetFormByIdAsync(int formId)
        {
            var result = await _formService.GetFormByIdAsync(formId);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost("{formId}/fields/create")]
        public async Task<ActionResult<Result<Field>>> CreateFieldAsync(int formId, string name, FieldType type)
        {
            return await _formService.CreateFieldAsync(name, type, formId);
        }

        [HttpPost("{formId}/samples/create")]
        public async Task<ActionResult<Result<FormSample>>> CreateFormSampleAsync(int formId, IFormFile image)
        {
            var result = await _formService.CreateFormSampleAsync(formId, image);
            if (!result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
