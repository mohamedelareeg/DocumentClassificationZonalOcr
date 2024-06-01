using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormSampleService
    {
        Task<Result<bool>> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage);
        Task<Result<IEnumerable<Zone>>> GetAllZonesAsync(int formSampleId);
        Task<Result<bool>> AddZoneAsync(int formSampleId, ZoneDto zone);
        Task<Result<FormSample>> GetFormSampleByIdAsync(int formSampleId);
    }
}
