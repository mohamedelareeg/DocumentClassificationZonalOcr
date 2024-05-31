using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IZoneRepository
    {
        Task<Result<Zone>> GetByIdAsync(int id);
        Task<Result<Zone>> CreateAsync(Zone zone);
        Task<Result<bool>> UpdateAsync(Zone zone);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<IEnumerable<Zone>>> GetAllByFormSampleIdAsync(int formSampleId);

    }
}
