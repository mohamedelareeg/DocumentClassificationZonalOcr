using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IOcrService
    {
        Task<Result<List<OcrValuesDto>>> OcrImageAsync(int formSampleId, Bitmap bitmap, FormDetectionSetting formDetectionSetting);
    }
}
