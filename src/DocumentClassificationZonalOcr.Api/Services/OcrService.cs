using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class OcrService : IOcrService
    {
        private readonly IFormSampleRepository _formSampleRepository;

        public OcrService(IFormSampleRepository formSampleRepository)
        {
            _formSampleRepository = formSampleRepository;
        }

        public async Task<Result<bool>> OcrImageAsync(int formSampleId, Bitmap bitmap, FormDetectionSetting formDetectionSetting)
        {
            var formSampleResult = await _formSampleRepository.GetByIdAsync(formSampleId);
            if (formSampleResult.IsFailure)
                return Result.Failure<bool>(formSampleResult.Error);

            var formSample = formSampleResult.Value;
            
            //TODO EXTRACT DATA AND SAVE IT
            return true;

        }
    }
}
