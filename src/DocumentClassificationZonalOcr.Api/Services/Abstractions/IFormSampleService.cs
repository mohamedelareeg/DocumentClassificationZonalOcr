using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormSampleService
    {
        Task<Result<bool>> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage);
        Task<Result<IEnumerable<ZoneDto>>> GetAllZonesAsync(int formSampleId);
        Task<Result<bool>> AddZoneAsync(int formSampleId, ZoneRequestDto zone);
        Task<Result<FormSampleDto>> GetFormSampleByIdAsync(int formSampleId);
        Task<Result<bool>> RemoveFormSampleAsync(int formSampleId);
        Task<Result<bool>> RemoveAllZonesAsync(int formSampleId);
    }
}
