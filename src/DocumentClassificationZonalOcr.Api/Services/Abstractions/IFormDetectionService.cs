using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormDetectionService
    {
        Task<Result<bool>> DetectFormAsync(Bitmap image, FormDetectionSetting formDetectionSetting);
    }
}
