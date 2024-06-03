using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using System.Drawing;
using Tesseract;
using System.Threading.Tasks;
using System;
using DocumentClassificationZonalOcr.Shared.Dtos;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class OcrService : IOcrService
    {
        private readonly IFormSampleRepository _formSampleRepository;
        private readonly IImageEnhancementService _imageEnhancementService;


        #region TesseractOcr
        private static readonly string tessdataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");
        private static readonly string Language = "ara+eng";
        private static readonly TesseractEngine _engine = new TesseractEngine(tessdataPath, Language, EngineMode.Default);
        #endregion
        public OcrService(IFormSampleRepository formSampleRepository, IImageEnhancementService imageEnhancementService)
        {
            _formSampleRepository = formSampleRepository;
            _imageEnhancementService = imageEnhancementService;
        }

        public async Task<Result<List<OcrValuesDto>>> OcrImageAsync(int formSampleId, Bitmap processedImage, FormDetectionSetting formDetectionSetting)
        {
            if (formSampleId <= 0)
                return Result.Failure<List<OcrValuesDto>>("Invalid form sample ID.");

            if (processedImage == null)
                return Result.Failure<List<OcrValuesDto>>("Processed image cannot be null.");

            if (formDetectionSetting == null)
                return Result.Failure<List<OcrValuesDto>>("Form detection setting cannot be null.");

            var formSampleResult = await _formSampleRepository.GetByIdAsync(formSampleId);
            if (formSampleResult.IsFailure)
                return Result.Failure<List<OcrValuesDto>>(formSampleResult.Error);

            var formSample = formSampleResult.Value;

            var dtos = new List<OcrValuesDto>();
            foreach (var zone in formSample.Zones)
            {
                if (zone.IsAnchorPoint)
                {
                    continue;
                }
                var bitmapCopy = processedImage;
                if (zone.ActualWidth > 0 && zone.ActualHeight > 0)
                {
                    double resizeFactorX = zone.ActualImageWidth / (double)bitmapCopy.Width;
                    double resizeFactorY = zone.ActualImageHeight / (double)bitmapCopy.Height;
                    var targetWidth = (int)(bitmapCopy.Width * resizeFactorX);
                    var targetHeight = (int)(bitmapCopy.Height * resizeFactorY);
                    var resizedImageResult = await _imageEnhancementService.ResizeImageAsync(bitmapCopy, resizeFactorX, resizeFactorY);
                    if (resizedImageResult.IsFailure)
                        return Result.Failure<List<OcrValuesDto>>(resizedImageResult.Error);

                    bitmapCopy = resizedImageResult.Value;

                    var croppedImageResult = await _imageEnhancementService.CropImageAsync(bitmapCopy, zone.X, zone.Y, zone.ActualWidth, zone.ActualHeight);
                    if (croppedImageResult.IsFailure)
                        return Result.Failure<List<OcrValuesDto>>(croppedImageResult.Error);

                    bitmapCopy = croppedImageResult.Value;
                }

                string value = string.Empty;
                switch (formDetectionSetting.OcrEngine)
                {
                    case Shared.Enums.OcrEngine.TesseractOcr:
                        var TesseractOcrResult = await TesseractOcrAsync(bitmapCopy, zone);
                        if (TesseractOcrResult.IsFailure)
                            return Result.Failure<List<OcrValuesDto>>(TesseractOcrResult.Error);
                        value = TesseractOcrResult.Value;
                        break;
                    case Shared.Enums.OcrEngine.IrisOcr:
                        var IrisOcrResult = await IrisOcrAsync(bitmapCopy, zone);
                        if (IrisOcrResult.IsFailure)
                            return Result.Failure<List<OcrValuesDto>>(IrisOcrResult.Error);
                        value = IrisOcrResult.Value;
                        break;
                    case Shared.Enums.OcrEngine.ABBYY:
                        var ABBYYResult = await ABBYYAsync(bitmapCopy, zone);
                        if (ABBYYResult.IsFailure)
                            return Result.Failure<List<OcrValuesDto>>(ABBYYResult.Error);
                        value = ABBYYResult.Value;
                        break;
                    default:
                        return Result.Failure<List<OcrValuesDto>>("Unsupported OCR engine specified.");
                }
                if (!string.IsNullOrEmpty(value))
                {
                    var dto = new OcrValuesDto { FieldId= (int)zone.FieldId, Value = value };
                    dtos.Add(dto);
                }
               
            }
            return dtos;
        }

        private async Task<Result<string>> ABBYYAsync(Bitmap bitmapCopy, Zone zone)
        {

            return Result.Success("");
        }

        private async Task<Result<string>> IrisOcrAsync(Bitmap bitmapCopy, Zone zone)
        {

            return Result.Success("");
        }

        private async Task<Result<string>> TesseractOcrAsync(Bitmap bitmapCopy, Zone zone)
        {
            using (var engine = new TesseractEngine(tessdataPath, Language, EngineMode.Default))
            {
                if (zone.WhiteList != null)
                {
                    engine.SetVariable("tessedit_char_whitelist", zone.WhiteList);
                }
                else
                {
                    engine.SetVariable("tessedit_char_whitelist", "");
                }

                using (var img = PixConverter.ToPix(bitmapCopy))
                using (var page = engine.Process(img))
                {
                    return Result.Success(page.GetText().Trim());
                }
            }
        }
    }
}
