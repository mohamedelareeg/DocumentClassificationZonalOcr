using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormDetectionSettingService : IFormDetectionSettingService
    {
        private readonly IFormDetectionSettingsRepository _formDetectionSettingsRepository;
        public FormDetectionSettingService(IFormDetectionSettingsRepository formDetectionSettingsRepository)
        {
            _formDetectionSettingsRepository = formDetectionSettingsRepository;
        }
        public async Task<Result<FormDetectionSetting>> GetSettingsAsync()
        {
            var result = await _formDetectionSettingsRepository.GetSettingsAsync();
            if (result.IsFailure)
                return Result.Failure<FormDetectionSetting>(result.Error);

            return Result.Success(result.Value);
        }

        public async Task<Result<bool>> UpdateSettingsAsync(FormDetectionSettingDto settingsDto)
        {
            var existingSettingsResult = await _formDetectionSettingsRepository.GetSettingsAsync();
            if (existingSettingsResult.IsFailure)
                return Result.Failure<bool>(existingSettingsResult.Error);

            var existingSettings = existingSettingsResult.Value;

            var modifyResult = existingSettings.Modify(
                settingsDto.OcrEngine,
                settingsDto.FormSimilarity,
                settingsDto.DetectOptions,
                settingsDto.ZoneAllowedWidth,
                settingsDto.ZoneAllowedHeight,
                settingsDto.DetectAlgorithm,
                settingsDto.ActivePerspectiveTransform,
                settingsDto.ResizeImage,
                settingsDto.ConvertToGrayscale,
                settingsDto.Normalization,
                settingsDto.Blurring,
                settingsDto.EdgeDetection,
                settingsDto.HistogramEqualization);

            if (modifyResult.IsFailure)
                return Result.Failure<bool>(modifyResult.Error);

            var updateResult = await _formDetectionSettingsRepository.UpdateSettingsAsync(existingSettings);
            if (updateResult.IsFailure)
                return Result.Failure<bool>(updateResult.Error);

            return Result.Success(true);
        }

   
    }
}
