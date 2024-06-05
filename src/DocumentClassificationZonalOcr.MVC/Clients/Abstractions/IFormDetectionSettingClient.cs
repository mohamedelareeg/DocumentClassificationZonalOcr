using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.Shared.Dtos;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IFormDetectionSettingClient
    {
        Task<BaseResponse<FormDetectionSettingDto>> GetSettingsAsync();
        Task<BaseResponse<bool>> UpdateSettingsAsync(FormDetectionSettingDto settings);
    }
}
