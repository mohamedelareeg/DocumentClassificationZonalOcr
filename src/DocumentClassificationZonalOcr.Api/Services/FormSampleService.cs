using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.MappingExtensions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormSampleService : IFormSampleService
    {
        private readonly IFormSampleRepository _formSampleRepository;
        private readonly IZoneRepository _zoneRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormSampleService(IFormSampleRepository formSampleRepository, IZoneRepository zoneRepository, IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _formSampleRepository = formSampleRepository;
            _zoneRepository = zoneRepository;
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<bool>> ModifyFormSampleImageAsync(int formSampleId, IFormFile newImage)
        {
            if (newImage == null || newImage.Length == 0)
                return Result.Failure<bool>("ModifyFormSampleImageAsync", "New image is required.");

            var formSampleResult = await _formSampleRepository.GetByIdAsync(formSampleId);
            if (formSampleResult.IsFailure)
                return Result.Failure<bool>(formSampleResult.Error);

            var formSample = formSampleResult.Value;

            string uploadsFolder = Path.GetDirectoryName(formSample.ImagePath);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;
            string newFilePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(newFilePath, FileMode.Create))
            {
                await newImage.CopyToAsync(fileStream);
            }
            var updateFormSampleResult = formSample.ModifyImagePath(newFilePath);
            if (updateFormSampleResult.IsFailure)
                return Result.Failure<bool>(updateFormSampleResult.Error);

            var updateResult = await _formSampleRepository.UpdateAsync(updateFormSampleResult.Value);
            if (!updateResult.IsFailure)
                return Result.Failure<bool>(updateResult.Error);

            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<ZoneDto>>> GetAllZonesAsync(int formSampleId)
        {
            var getZonesResult = await _zoneRepository.GetAllByFormSampleIdAsync(formSampleId);
            if (getZonesResult.IsFailure)
                return Result.Failure<IEnumerable<ZoneDto>>(getZonesResult.Error);


            var dtos = getZonesResult.Value.Select(dto => dto.ToDto());

            return Result.Success(dtos);
        }

        public async Task<Result<bool>> AddZoneAsync(int formSampleId, ZoneRequestDto zoneDto)
        {
            var createZoneResult = Zone.Create(
                zoneDto.X,
                zoneDto.Y,
                zoneDto.ActualWidth,
                zoneDto.ActualHeight,
                zoneDto.ActualImageWidth,
                zoneDto.ActualImageHeight,
                formSampleId,
                zoneDto.FieldId,
                zoneDto.Regex,
                zoneDto.WhiteList,
                zoneDto.IsDuplicated,
                zoneDto.ZoneFieldType,
                zoneDto.IsAnchorPoint);

            if (createZoneResult.IsFailure)
                return Result.Failure<bool>(createZoneResult.Error);

            var result = await _zoneRepository.CreateAsync(createZoneResult.Value);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            if (zoneDto.IsAnchorPoint)
            {
                var cropResult = await CropAncherPointImage(formSampleId, result.Value);
                if (cropResult.IsFailure)
                    return Result.Failure<bool>(cropResult.Error);
            }

            return Result.Success(true);
        }

        public async Task<Result<bool>> CropAncherPointImage(int formSampleId, Zone zone)
        {
            var formSampleResult = await _formSampleRepository.GetByIdAsync(formSampleId);
            if (formSampleResult.IsFailure)
                return Result.Failure<bool>(formSampleResult.Error);

            var formSample = formSampleResult.Value;

            string imagePath = formSample.ImagePath;
            imagePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
            using (var image = Image.Load(imagePath))
            {
                var croppedImage = image.Clone(ctx => ctx.Crop(new Rectangle(
                    (int)zone.X,
                    (int)zone.Y,
                    (int)zone.ActualWidth,
                    (int)zone.ActualHeight)));

                string outputDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "forms", formSample.FormId.ToString(), "AnchorPoints");
                string uniqueFileName = $"{Guid.NewGuid()}.jpg";
                string outputPath = Path.Combine(outputDirectory, uniqueFileName);

                if (!Directory.Exists(outputDirectory))
                    Directory.CreateDirectory(outputDirectory);

                using (var outputStream = new FileStream(outputPath, FileMode.Create))
                {
                    croppedImage.Save(outputStream, new JpegEncoder());
                }

                var modifyResult = zone.ModifyAnchorPointPath(outputPath);
                if (modifyResult.IsFailure)
                    return Result.Failure<bool>(modifyResult.Error);

                var updateZoneResult = await _zoneRepository.UpdateAsync(zone);
                if (updateZoneResult.IsFailure)
                    return Result.Failure<bool>(updateZoneResult.Error);

                return Result.Success(true);
            }
        }

        public async Task<Result<FormSampleDto>> GetFormSampleByIdAsync(int formSampleId)
        {
            var result = await _formSampleRepository.GetByIdAsync(formSampleId);
            if (result.IsFailure)
                return Result.Failure<FormSampleDto>(result.Error);

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";

            var dto = result.Value.ToDto(baseUrl);
            return Result.Success(dto);
        }

        public async Task<Result<bool>> RemoveFormSampleAsync(int formSampleId)
        {
            var result = await _formSampleRepository.DeleteAsync(formSampleId);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);
            return Result.Success(true);
        }

        public async Task<Result<bool>> RemoveAllZonesAsync(int formSampleId)
        {
            var zonesResult = await _zoneRepository.GetAllByFormSampleIdAsync(formSampleId);
            if (zonesResult.IsFailure)
                return Result.Failure<bool>(zonesResult.Error);

            foreach (var zone in zonesResult.Value)
            {
                var deleteResult = await _zoneRepository.DeleteAsync(zone.Id);
                if (deleteResult.IsFailure)
                    return Result.Failure<bool>(deleteResult.Error);
            }

            return Result.Success(true);
        }
    }
}
