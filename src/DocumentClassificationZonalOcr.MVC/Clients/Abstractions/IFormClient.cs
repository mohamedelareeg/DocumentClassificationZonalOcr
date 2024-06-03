using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormClient
    {
        Task<BaseResponse<FormDto>> CreateFormAsync(string name);
        Task<BaseResponse<bool>> ModifyFormNameAsync(int formId, string newName);
        Task<BaseResponse<bool>> RemoveFormAsync(int formId);
        Task<BaseResponse<bool>> AddFieldToFormAsync(int formId, FieldRequestDto field);
        Task<BaseResponse<bool>> AddSampleToFormAsync(int formId, FormSampleRequestDto sample);
        Task<BaseResponse<CustomList<FieldDto>>> GetAllFormFieldsAsync(int formId, DataTableOptionsDto options);
        Task<BaseResponse<CustomList<FormSampleDto>>> GetAllFormSamplesAsync(int formId, DataTableOptionsDto options);
        Task<BaseResponse<FormDto>> GetFormByIdAsync(int formId);
        Task<BaseResponse<bool>> CreateFieldAsync(int formId, string name, FieldType type);
        Task<BaseResponse<bool>> CreateFormSampleAsync(int formId, IFormFile image);
        Task<BaseResponse<CustomList<FormDto>>> GetAllFormsAsync(DataTableOptionsDto options);

    }
}
