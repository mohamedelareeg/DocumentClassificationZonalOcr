using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using System.Drawing;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormDetection : IFormDetection
    {
        public async Task<Result<bool>> DetectForm(Bitmap image)
        {

            return true;
        }
    }
}
