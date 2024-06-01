using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IImageEnhancementService
    {
        Task<Result<Bitmap>> DoPerspectiveTransformBitmapAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ResizeImageAsync(Bitmap inputBitmap, int width, int height);
        Task<Result<Bitmap>> ConvertToGrayscaleAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyNormalizationAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyBlurringAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyEdgeDetectionAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyHistogramEqualizationAsync(Bitmap inputBitmap);
    }
}
