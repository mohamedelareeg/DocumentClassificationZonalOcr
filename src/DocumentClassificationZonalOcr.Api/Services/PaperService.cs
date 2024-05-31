using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
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

        public PaperService(
            IPaperRepository paperRepository,
            IExportedMetaDataRepository exportedMetaDataRepository,
            IImageEnhancementService imageEnhancementService)
        {
            _paperRepository = paperRepository;
            _exportedMetaDataRepository = exportedMetaDataRepository;
            _imageEnhancementService = imageEnhancementService;
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

        private async Task<Result<bool>> ProcessSingleImageAsync(IFormFile image)
        {
            if (image == null || image.Length == 0)
                return Result.Failure<bool>("ProcessSingleImageAsync", "Invalid image file.");

            using (var stream = new MemoryStream())
            {
                await image.CopyToAsync(stream);

                stream.Seek(0, SeekOrigin.Begin);
                var bitmap = new Bitmap(stream);

                var result = await _imageEnhancementService.DoPerspectiveTransformBitmapAsync(bitmap);

                if (result.IsFailure)
                    return Result.Failure<bool>(result.Error);

                var modifiedBitmap = result.Value;

                return Result.Success(true);
            }
        }


        public async Task<Result<bool>> ProcessImageAsync(IFormFile image)
        {
            var result = await ProcessSingleImageAsync(image);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            var transformedImage = result.Value;

            return Result.Success(true);
        }

        public async Task<Result<bool>> ProcessImagesAsync(List<IFormFile> images)
        {
            if (images == null || !images.Any())
                return Result.Failure<bool>("No images provided.");

            foreach (var image in images)
            {
                var result = await ProcessSingleImageAsync(image);
                if (result.IsFailure)
                    return Result.Failure<bool>(result.Error);

                var transformedImage = result.Value;
            }

            return Result.Success(true);
        }
    }
}
