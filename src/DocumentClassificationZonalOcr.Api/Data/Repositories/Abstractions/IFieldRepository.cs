using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IFieldRepository
    {
        Task<Result<Field>> GetByIdAsync(int id);
        Task<Result<Field>> CreateAsync(Field field);
        Task<Result<bool>> UpdateAsync(Field field);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
