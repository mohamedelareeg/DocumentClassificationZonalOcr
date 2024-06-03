using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Shared.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class PaperService : IPaperService
    {
        private readonly IPaperRepository _paperRepository;
        private readonly IExportedMetaDataRepository _exportedMetaDataRepository;
        private readonly IImageEnhancementService _imageEnhancementService;
        private readonly IFormDetectionSettingsRepository _formDetectionSettingsRepository;
        private readonly IFormDetectionService _formDetectionService;
        private readonly IOcrService _ocrService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaperService(
            IPaperRepository paperRepository,
            IExportedMetaDataRepository exportedMetaDataRepository,
            IImageEnhancementService imageEnhancementService,
            IFormDetectionSettingsRepository formDetectionSettingsRepository,
            IFormDetectionService formDetectionService,
            IOcrService ocrService,
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor)
        {
            _paperRepository = paperRepository;
            _exportedMetaDataRepository = exportedMetaDataRepository;
            _imageEnhancementService = imageEnhancementService;
            _formDetectionSettingsRepository = formDetectionSettingsRepository;
            _formDetectionService = formDetectionService;
            _ocrService = ocrService;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<IEnumerable<Paper>>> GetAllPapersByFormIdAsync(int formId)
        {
            var result = await _paperRepository.GetAllByFormIdAsync(formId);
            if (result.IsFailure)
                return Result.Failure<IEnumerable<Paper>>(result.Error);

            return Result.Success(result.Value);
        }

        public async Task<Result<IEnumerable<Paper>>> GetAllPapersByDocumentIdAsync(int documentId)
        {
            var result = await _paperRepository.GetAllByDocumentIdAsync(documentId);
            if (result.IsFailure)
                return Result.Failure<IEnumerable<Paper>>(result.Error);

            return Result.Success(result.Value);
        }

        public async Task<Result<IEnumerable<ExportedMetaData>>> GetAllPaperMetadataAsync(int paperId)
        {
            var result = await _exportedMetaDataRepository.GetAllByPaperIdAsync(paperId);
            if (result.IsFailure)
                return Result.Failure<IEnumerable<ExportedMetaData>>(result.Error);

            return Result.Success(result.Value);
        }

        private async Task<Result<bool>> ProcessSingleImageAsync(IFormFile image, FormDetectionSetting formDetectionSetting)
        {
            if (image == null || image.Length == 0)
                return Result.Failure<bool>("ProcessSingleImageAsync", "Invalid image file.");

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);

                var bitmap = new Bitmap(stream);

                if (formDetectionSetting.ActivePerspectiveTransform)
                {
                    var perspectiveResult = await _imageEnhancementService.DoPerspectiveTransformBitmapAsync(bitmap);

                    if (perspectiveResult.IsFailure)
                        return Result.Failure<bool>(perspectiveResult.Error);

                    bitmap = perspectiveResult.Value;
                }

                if (formDetectionSetting.ConvertToGrayscale)
                {
                    var grayscaleResult = await _imageEnhancementService.ConvertToGrayscaleAsync(bitmap);

                    if (grayscaleResult.IsFailure)
                        return Result.Failure<bool>(grayscaleResult.Error);

                    bitmap = grayscaleResult.Value;
                }
                
                if (formDetectionSetting.Blurring)
                {
                    var blurResult = await _imageEnhancementService.ApplyBlurringAsync(bitmap);

                    if (blurResult.IsFailure)
                        return Result.Failure<bool>(blurResult.Error);

                    bitmap = blurResult.Value;
                }

                if (formDetectionSetting.EdgeDetection)
                {
                    var edgeDetectionResult = await _imageEnhancementService.ApplyEdgeDetectionAsync(bitmap);

                    if (edgeDetectionResult.IsFailure)
                        return Result.Failure<bool>(edgeDetectionResult.Error);

                    bitmap = edgeDetectionResult.Value;
                }

                if (formDetectionSetting.HistogramEqualization)
                {
                    var histogramEqualizationResult = await _imageEnhancementService.ApplyHistogramEqualizationAsync(bitmap);

                    if (histogramEqualizationResult.IsFailure)
                        return Result.Failure<bool>(histogramEqualizationResult.Error);

                    bitmap = histogramEqualizationResult.Value;
                }

                if (formDetectionSetting.Normalization)
                {
                    var normalizationResult = await _imageEnhancementService.ApplyNormalizationAsync(bitmap);

                    if (normalizationResult.IsFailure)
                        return Result.Failure<bool>(normalizationResult.Error);

                    bitmap = normalizationResult.Value;
                }

                var formDetectionResult = await _formDetectionService.DetectFormAsync(bitmap, formDetectionSetting);
                if (formDetectionResult.IsFailure)
                    return Result.Failure<bool>(formDetectionResult.Error);

                if (formDetectionResult.Value == 0)
                {
                    return Result.Success(true);
                }

                var formSampleId = formDetectionResult.Value;


         

                var ocrValuesResult = await _ocrService.OcrImageAsync(formSampleId , bitmap, formDetectionSetting);
                if (ocrValuesResult.IsFailure)
                    return Result.Failure<bool>(ocrValuesResult.Error);

                var ocrValues = ocrValuesResult.Value;

                string outputDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "papers");
                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }
                string uniqueFileName = $"{Guid.NewGuid()}.jpg";
                string outputPath = Path.Combine(outputDirectory, uniqueFileName);
                string filePath = Path.Combine("papers", uniqueFileName);

                using (var fileStream = new FileStream(outputPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                var createPaperResult = Paper.Create(filePath, uniqueFileName);
                if (createPaperResult.IsFailure)
                    return Result.Failure<bool>(createPaperResult.Error);

                var createPaper = createPaperResult.Value;

                var addPaperResult = await _paperRepository.CreateAsync(createPaper);
                if (addPaperResult.IsFailure)
                    return Result.Failure<bool>(addPaperResult.Error);


                foreach (var ocrValue in ocrValues)
                {
                    var createExportedMetadataResult = ExportedMetaData.Create(ocrValue.FieldId,ocrValue.Value,createPaper.Id);
                    if (createExportedMetadataResult.IsFailure)
                        return Result.Failure<bool>(createExportedMetadataResult.Error);

                    var createExportedMetadata = createExportedMetadataResult.Value;

                    var addExportedMetadataResult = await _exportedMetaDataRepository.AddMetadataAsync(createExportedMetadata);
                    if (addExportedMetadataResult.IsFailure)
                        return Result.Failure<bool>(addExportedMetadataResult.Error);
                }


            }

            return Result.Success(true);
        }



        public async Task<Result<bool>> ProcessImageAsync(IFormFile image)
        {
            var formDetectionSettingsResult = await _formDetectionSettingsRepository.GetSettingsAsync();
            if (formDetectionSettingsResult.IsFailure)
                return Result.Failure<bool>(formDetectionSettingsResult.Error);

            var formDetectionSettings = formDetectionSettingsResult.Value;
            var result = await ProcessSingleImageAsync(image, formDetectionSettings);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            var transformedImage = result.Value;

            return Result.Success(true);
        }

        public async Task<Result<bool>> ProcessImagesAsync(List<IFormFile> images)
        {
            if (images == null || !images.Any())
                return Result.Failure<bool>("No images provided.");

            var formDetectionSettingsResult = await _formDetectionSettingsRepository.GetSettingsAsync();
            if (formDetectionSettingsResult.IsFailure)
                return Result.Failure<bool>(formDetectionSettingsResult.Error);

            var formDetectionSettings = formDetectionSettingsResult.Value;
            if(formDetectionSettingsResult.Value is null)
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
                    return Result.Failure<bool>(defaultSettings.Error);

                var addSettingsResult = await _formDetectionSettingsRepository.UpdateSettingsAsync(defaultSettings.Value);
                if (addSettingsResult.IsFailure)
                    return Result.Failure<bool>(addSettingsResult.Error);

                formDetectionSettingsResult = await _formDetectionSettingsRepository.GetSettingsAsync();
                if (formDetectionSettingsResult.IsFailure)
                    return Result.Failure<bool>(formDetectionSettingsResult.Error);

                formDetectionSettings = formDetectionSettingsResult.Value;
            }
            foreach (var image in images)
            {
                var result = await ProcessSingleImageAsync(image, formDetectionSettings);
                if (result.IsFailure)
                    return Result.Failure<bool>(result.Error);

                var transformedImage = result.Value;
            }

            return Result.Success(true);
        }

        public async Task<Result<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options)
        {
            var result = await _paperRepository.GetAllPapersAsync(options);
            if (result.IsFailure)
                return Result.Failure<CustomList<PaperDto>>(result.Error);

            return Result.Success(result.Value);
        }
    }
}
