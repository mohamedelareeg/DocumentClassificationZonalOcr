using CaptureSolution.AutomaticReleaseApi.Controllers.Base;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class FormController : AppControllerBase
    {
        private readonly IFormService _formService;

        public FormController(IFormService formService)
        {
            _formService = formService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateFormAsync([FromBody] string name)
        {
            var result = await _formService.CreateFormAsync(name);
            return CustomResult(result);
        }

        [HttpPut("modify/{formId}")]
        public async Task<IActionResult> ModifyFormNameAsync(int formId, [FromBody] string newName)
        {
            var result = await _formService.ModifyFormNameAsync(formId, newName);
            return CustomResult(result);
        }

        [HttpDelete("remove/{formId}")]
        public async Task<IActionResult> RemoveFormAsync(int formId)
        {
            var result = await _formService.RemoveFormAsync(formId);
            return CustomResult(result);
        }

        [HttpPost("{formId}/fields/add")]
        public async Task<IActionResult> AddFieldToFormAsync(int formId, [FromBody] FieldRequestDto field)
        {
            var result = await _formService.AddFieldToFormAsync(formId, field);
            return CustomResult(result);
        }

        [HttpPost("{formId}/samples/add")]
        public async Task<IActionResult> AddSampleToFormAsync(int formId, IFormFile file)
        {
            var result = await _formService.AddSampleToFormAsync(formId, file, null);
            return CustomResult(result);
        }

        [HttpPost("{formId}/fields")]
        public async Task<IActionResult> GetAllFormFieldsAsync(int formId, [FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormFieldsAsync(formId, options);
            return CustomResult(result);
        }

        [HttpPost("{formId}/samples")]
        public async Task<IActionResult> GetAllFormSamplesAsync(int formId, [FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormSamplesAsync(formId, options);
            return CustomResult(result);
        }

        [HttpGet("get/{formId}")]
        public async Task<IActionResult> GetFormByIdAsync(int formId)
        {
            var result = await _formService.GetFormByIdAsync(formId);
            return CustomResult(result);
        }

        [HttpPost("all")]
        public async Task<IActionResult> GetAllFormsAsync([FromBody] DataTableOptionsDto options)
        {
            var result = await _formService.GetAllFormsAsync(options);
            return CustomResult(result);
        }

        [HttpPost("{formId}/fields/create")]
        public async Task<IActionResult> CreateFieldAsync(int formId, string name, FieldType type)
        {
            var result = await _formService.CreateFieldAsync(name, type, formId);
            return CustomResult(result);
        }

        [HttpPost("{formId}/samples/create")]
        public async Task<IActionResult> CreateFormSampleAsync(int formId, IFormFile image)
        {
            var result = await _formService.CreateFormSampleAsync(formId, image);
            return CustomResult(result);
        }
    }
}
