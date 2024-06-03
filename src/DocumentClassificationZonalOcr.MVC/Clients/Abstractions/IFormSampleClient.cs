using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormSampleClient
    {
        Task<BaseResponse<bool>> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage);
        Task<BaseResponse<IEnumerable<ZoneDto>>> GetAllZonesAsync(int formSampleId);
        Task<BaseResponse<bool>> AddZoneAsync(int formSampleId, ZoneRequestDto zone);
        Task<BaseResponse<FormSampleDto>> GetFormSampleByIdAsync(int formSampleId);
        Task<BaseResponse<bool>> RemoveFormSampleAsync(int fieldId);
        Task<BaseResponse<bool>> RemoveAllZonesAsync(int formSampleId);

    }
}
