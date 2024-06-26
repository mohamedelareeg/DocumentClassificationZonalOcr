﻿using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Enums;

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
            var formDetectionSettings = result.Value;
            if (result.Value is null)
            {
                var defaultSettings = FormDetectionSetting.Create(
                      OcrEngine.TesseractOcr,
                      5,
                      DetectOptions.FullImage,
                      0,
                      0,
                      DetectAlgorithm.SIFT,
                      false,
                      false,
                      false,
                      false,
                      false,
                      false,
                      false
                  );
                if (defaultSettings.IsFailure)
                    return Result.Failure<FormDetectionSetting>(defaultSettings.Error);

                var addSettingsResult = await _formDetectionSettingsRepository.UpdateSettingsAsync(defaultSettings.Value);
                if (addSettingsResult.IsFailure)
                    return Result.Failure<FormDetectionSetting>(addSettingsResult.Error);

                result = await _formDetectionSettingsRepository.GetSettingsAsync();
                if (result.IsFailure)
                    return Result.Failure<FormDetectionSetting>(result.Error);

                formDetectionSettings = result.Value;
            }
            return Result.Success(formDetectionSettings);
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
