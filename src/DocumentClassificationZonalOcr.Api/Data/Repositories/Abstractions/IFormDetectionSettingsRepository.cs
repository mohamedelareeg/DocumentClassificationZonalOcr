using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IFormDetectionSettingsRepository
    {
        Task<Result<FormDetectionSetting>> GetSettingsAsync();
        Task<Result<bool>> UpdateSettingsAsync(FormDetectionSetting settings);
    }
}
