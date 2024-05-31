using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IFormSampleRepository
    {
        Task<Result<FormSample>> GetByIdAsync(int id);
        Task<Result<FormSample>> CreateAsync(FormSample formSample);
        Task<Result<bool>> UpdateAsync(FormSample formSample);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
