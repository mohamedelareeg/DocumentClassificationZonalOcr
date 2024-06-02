using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormClient
    {
        Task<FormDto> CreateFormAsync(string name);
        Task<bool> ModifyFormNameAsync(int formId, string newName);
        Task<bool> RemoveFormAsync(int formId);
        Task<bool> AddFieldToFormAsync(int formId, FieldRequestDto field);
        Task<bool> AddSampleToFormAsync(int formId, FormSampleRequestDto sample);
        Task<CustomList<FieldDto>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options);
        Task<CustomList<FormSampleDto>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options);
        Task<FormDto> GetFormByIdAsync(int formId);
        Task<bool> CreateFieldAsync(int formId, string name, FieldType type);
        Task<bool> CreateFormSampleAsync(int formId, IFormFile image);
        Task<CustomList<FormDto>> GetAllFormsAsync(DataTableOptionsDto options);

    }
}
