using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IImageEnhancementService
    {
        Task<Result<Bitmap>> DoPerspectiveTransformBitmapAsync(Bitmap inputBitmap);
    }
}
