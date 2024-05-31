using DocumentClassificationZonalOcr.Api.Results;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IFormDetection
    {
        Task<Result<bool>> DetectForm(Bitmap image);
    }
}
