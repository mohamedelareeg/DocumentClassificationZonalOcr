using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormSampleClient
    {
        Task<bool> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage);
        Task<IEnumerable<ZoneDto>> GetAllZonesAsync(int formSampleId);
        Task<bool> AddZoneAsync(int formSampleId, ZoneRequestDto zone);
        Task<FormSampleDto> GetFormSampleByIdAsync(int formSampleId);
        Task<bool> RemoveFormSampleAsync(int fieldId);
    }
}
