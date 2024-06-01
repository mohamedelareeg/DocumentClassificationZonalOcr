using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormDetectionSettingService
    {
        Task<Result<bool>> UpdateSettingsAsync(FormDetectionSettingDto settings);
        Task<Result<FormDetectionSetting>> GetSettingsAsync();
    }
}
