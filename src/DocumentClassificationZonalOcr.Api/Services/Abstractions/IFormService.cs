using DocumentClassificationZonalOcr.Api.Dtos;
using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormService
    {
        Task<Result<Form>> CreateFormAsync(string name);
        Task<Result<bool>> ModifyFormNameAsync(int formId, string newName);
        Task<Result<bool>> RemoveFormAsync(int formId);
        Task<Result<bool>> AddFieldToFormAsync(int formId, Field field);
        Task<Result<bool>> AddSampleToFormAsync(int formId, FormSample sample);
        Task<Result<IEnumerable<Field>>> GetAllFormFieldsAsync(int formId);
        Task<Result<IEnumerable<FormSample>>> GetAllFormSamplesAsync(int formId);
        Task<Result<Form>> GetFormByIdAsync(int formId);
        Task<Result<Field>> CreateFieldAsync(string name, FieldType type, int formId);
        Task<Result<FormSample>> CreateFormSampleAsync(int formId, IFormFile image);
    }
}
