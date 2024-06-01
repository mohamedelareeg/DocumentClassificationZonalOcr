using DocumentClassificationZonalOcr.MVC.Base;
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
        Task<IEnumerable<FieldDto>> GetAllFormFieldsAsync(int formId);
        Task<IEnumerable<FormSampleDto>> GetAllFormSamplesAsync(int formId);
        Task<FormDto> GetFormByIdAsync(int formId);
        Task<FieldDto> CreateFieldAsync(int formId, string name, FieldType type);
        Task<FormSampleDto> CreateFormSampleAsync(int formId, IFormFile image);
        Task<CustomList<FormDto>> GetAllFormsAsync(DataTableOptionsDto options);

    }
}
