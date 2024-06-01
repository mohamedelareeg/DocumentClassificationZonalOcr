using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepository;

        public FieldService(IFieldRepository fieldRepository)
        {
            _fieldRepository = fieldRepository;
        }

        public async Task<Result<bool>> ModifyFieldNameAsync(int fieldId, string newName)
        {
            var fieldResult = await _fieldRepository.GetByIdAsync(fieldId);
            if (fieldResult.IsFailure)
                return Result.Failure<bool>(fieldResult.Error);

            var field = fieldResult.Value;
            var result = field.ModifyName(newName);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            return await _fieldRepository.UpdateAsync(field);
        }

        public async Task<Result<bool>> ModifyFieldTypeAsync(int fieldId, FieldType newType)
        {
            var fieldResult = await _fieldRepository.GetByIdAsync(fieldId);
            if (fieldResult.IsFailure)
                return Result.Failure<bool>(fieldResult.Error);

            var field = fieldResult.Value;
            var result = field.ModifyType(newType);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            return await _fieldRepository.UpdateAsync(field);
        }

        public async Task<Result<Field>> GetFieldByIdAsync(int fieldId)
        {
            var result = await _fieldRepository.GetByIdAsync(fieldId);
            if (result.IsFailure)
                return Result.Failure<Field>(result.Error);

            return result.Value;
        }

        public async Task<Result<bool>> RemoveFieldAsync(int fieldId)
        {

            var result = await _fieldRepository.DeleteAsync(fieldId);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);
            return Result.Success(true);
        }
    }
}
