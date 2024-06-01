using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormService
    {
        Task<Result<FormDto>> CreateFormAsync(string name);
        Task<Result<bool>> ModifyFormNameAsync(int formId, string newName);
        Task<Result<bool>> RemoveFormAsync(int formId);
        Task<Result<bool>> AddFieldToFormAsync(int formId, FieldRequestDto field);
        Task<Result<bool>> AddSampleToFormAsync(int formId, FormSampleRequestDto sample);
        Task<Result<IEnumerable<FieldDto>>> GetAllFormFieldsAsync(int formId);
        Task<Result<IEnumerable<FormSampleDto>>> GetAllFormSamplesAsync(int formId);
        Task<Result<FormDto>> GetFormByIdAsync(int formId);
        Task<Result<FieldDto>> CreateFieldAsync(string name, FieldType type, int formId);
        Task<Result<FormSampleDto>> CreateFormSampleAsync(int formId, IFormFile image);
        Task<Result<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options);

    }
}
