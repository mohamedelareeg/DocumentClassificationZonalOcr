using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;
using Tesseract;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IImageEnhancementService
    {
        Task<Result<Bitmap>> DoPerspectiveTransformBitmapAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ResizeImageAsync(Bitmap inputBitmap, double width, double height);
        Task<Result<Bitmap>> ConvertToGrayscaleAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyNormalizationAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyBlurringAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyEdgeDetectionAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> ApplyHistogramEqualizationAsync(Bitmap inputBitmap);
        Task<Result<Bitmap>> CropImageAsync(Bitmap processedImage, double x, double y, double actualWidth, double actualHeight);
        Pix BitmapToPix(Bitmap processedImage);
    }
}
