using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IOcrService
    {
        Task<Result<bool>> OcrImageAsync(int formSampleId, Bitmap bitmap, FormDetectionSetting formDetectionSetting);
    }
}
