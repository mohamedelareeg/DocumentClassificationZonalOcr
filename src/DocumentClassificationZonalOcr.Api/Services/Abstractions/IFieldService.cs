using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFieldService
    {

        Task<Result<bool>> ModifyFieldNameAsync(int fieldId, string newName);
        Task<Result<bool>> ModifyFieldTypeAsync(int fieldId, FieldType newType);
        Task<Result<Field>> GetFieldByIdAsync(int fieldId);
        Task<Result<bool>> RemoveFieldAsync(int fieldId);
    }
}
