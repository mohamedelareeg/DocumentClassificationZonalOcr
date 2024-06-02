using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
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
        Task<Result<bool>> AddSampleToFormAsync(int formId, IFormFile file, List<ZoneRequestDto>? zones = null);
        Task<Result<CustomList<FieldDto>>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options);
        Task<Result<CustomList<FormSampleDto>>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options);
        Task<Result<FormDto>> GetFormByIdAsync(int formId);
        Task<Result<FieldDto>> CreateFieldAsync(string name, FieldType type, int formId);
        Task<Result<FormSampleDto>> CreateFormSampleAsync(int formId, IFormFile image);
        Task<Result<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options);

    }
}
