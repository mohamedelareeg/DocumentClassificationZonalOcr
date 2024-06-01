using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class FormDetectionSettingMappingExtensions
    {
        public static FormDetectionSettingDto ToDto(this FormDetectionSetting formDetectionSetting)
        {
            return new FormDetectionSettingDto
            {
                Id = formDetectionSetting.Id,
                OcrEngine = formDetectionSetting.OcrEngine,
                FormSimilarity = formDetectionSetting.FormSimilarity,
                DetectOptions = formDetectionSetting.DetectOptions,
                ZoneAllowedWidth = formDetectionSetting.ZoneAllowedWidth,
                ZoneAllowedHeight = formDetectionSetting.ZoneAllowedHeight,
                DetectAlgorithm = formDetectionSetting.DetectAlgorithm,
                ActivePerspectiveTransform = formDetectionSetting.ActivePerspectiveTransform,
                ResizeImage = formDetectionSetting.ResizeImage,
                ConvertToGrayscale = formDetectionSetting.ConvertToGrayscale,
                Normalization = formDetectionSetting.Normalization,
                Blurring = formDetectionSetting.Blurring,
                EdgeDetection = formDetectionSetting.EdgeDetection,
                HistogramEqualization = formDetectionSetting.HistogramEqualization
            };
        }

        public static FormDetectionSetting ToEntity(this FormDetectionSettingDto formDetectionSettingDto)
        {
            var formDetectionSetting = FormDetectionSetting.Create(
                formDetectionSettingDto.OcrEngine,
                formDetectionSettingDto.FormSimilarity,
                formDetectionSettingDto.DetectOptions,
                formDetectionSettingDto.ZoneAllowedWidth,
                formDetectionSettingDto.ZoneAllowedHeight,
                formDetectionSettingDto.DetectAlgorithm,
                formDetectionSettingDto.ActivePerspectiveTransform,
                formDetectionSettingDto.ResizeImage,
                formDetectionSettingDto.ConvertToGrayscale,
                formDetectionSettingDto.Normalization,
                formDetectionSettingDto.Blurring,
                formDetectionSettingDto.EdgeDetection,
                formDetectionSettingDto.HistogramEqualization
            );
            if (formDetectionSetting.IsFailure)
            {
                throw new Exception(formDetectionSetting.Error);
            }
            return formDetectionSetting.Value;
        }
        public static FormDetectionSetting ToEntity(this FormDetectionSettingRequestDto formDetectionSettingRequestDto)
        {
            var formDetectionSettingResult = FormDetectionSetting.Create(
                formDetectionSettingRequestDto.OcrEngine,
                formDetectionSettingRequestDto.FormSimilarity,
                formDetectionSettingRequestDto.DetectOptions,
                formDetectionSettingRequestDto.ZoneAllowedWidth,
                formDetectionSettingRequestDto.ZoneAllowedHeight,
                formDetectionSettingRequestDto.DetectAlgorithm,
                formDetectionSettingRequestDto.ActivePerspectiveTransform,
                formDetectionSettingRequestDto.ResizeImage,
                formDetectionSettingRequestDto.ConvertToGrayscale,
                formDetectionSettingRequestDto.Normalization,
                formDetectionSettingRequestDto.Blurring,
                formDetectionSettingRequestDto.EdgeDetection,
                formDetectionSettingRequestDto.HistogramEqualization
            );

            if (formDetectionSettingResult.IsFailure)
            {
                throw new Exception(formDetectionSettingResult.Error);
            }

            return formDetectionSettingResult.Value;
        }
    }
}
