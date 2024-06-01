using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using OpenCvSharp.Extensions;
using OpenCvSharp;
using System;
using System.Drawing;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class FormDetectionService : IFormDetectionService
    {
        private readonly IZoneRepository _zoneRepository;

        public FormDetectionService(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task<Result<bool>> DetectFormAsync(Bitmap image, FormDetectionSetting formDetectionSetting)
        {
            var anchorPointsResult = await _zoneRepository.GetAllAnchorPointsAsync();

            if (anchorPointsResult.IsFailure)
                return Result.Failure<bool>(anchorPointsResult.Error);

            var anchorPoints = anchorPointsResult.Value;
            using (var inputMat = BitmapConverter.ToMat(image))
            {
                foreach (var anchorPoint in anchorPoints)
                {
                    using (var anchorMat = Cv2.ImRead(anchorPoint.AnchorPointPath, ImreadModes.Color))
                    {
                        using (var result = inputMat.MatchTemplate(anchorMat, TemplateMatchModes.CCoeffNormed))
                        {
                            double minVal, maxVal;
                            OpenCvSharp.Point minLoc, maxLoc;

                            Cv2.MinMaxLoc(result, out minVal, out maxVal, out minLoc, out maxLoc);

                            double threshold = 0.8;

                            if (maxVal >= threshold)
                            {
                                int allowanceWidth = 100;
                                int allowanceHeight = 100;
                                int searchX = maxLoc.X - allowanceWidth;
                                int searchY = maxLoc.Y - allowanceHeight;
                                int searchWidth = anchorMat.Width + 2 * allowanceWidth;
                                int searchHeight = anchorMat.Height + 2 * allowanceHeight;

                                searchX = Math.Max(0, searchX);
                                searchY = Math.Max(0, searchY);
                                searchWidth = Math.Min(inputMat.Width - searchX, searchWidth);
                                searchHeight = Math.Min(inputMat.Height - searchY, searchHeight);
                                Rect searchRect = new Rect(searchX, searchY, searchWidth, searchHeight);

                                inputMat.Rectangle(searchRect, Scalar.Red, 2);

                                Console.WriteLine($"Anchor point found: {anchorPoint.AnchorPointPath}");
                                Console.WriteLine($"Position: {maxLoc}");
                                Console.WriteLine($"Search region: {searchRect}");
                            }
                        }
                    }
                }
            }

            return Result.Success(true);
        }
    }
}
