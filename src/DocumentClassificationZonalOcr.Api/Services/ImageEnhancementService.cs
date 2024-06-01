using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class ImageEnhancementService : IImageEnhancementService
    {

        private readonly ILogger<ImageEnhancementService> _logger;

        public ImageEnhancementService(ILogger<ImageEnhancementService> logger)
        {
            _logger = logger;
        }

        #region Helpers
        private byte[] inputBitmapToByteArray(Bitmap inputBitmap)
        {
            using (var stream = new MemoryStream())
            {
                inputBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                return stream.ToArray();
            }
        }
        private Bitmap RotateImageToPortrait(Bitmap bitmap)
        {
            Mat originalImage = BitmapConverter.ToMat(bitmap);

            if (originalImage.Empty())
            {
                _logger.LogError("Failed to load the image.");
                return null;
            }

            Mat gray = new Mat();
            Cv2.CvtColor(originalImage, gray, ColorConversionCodes.BGR2GRAY);

            Mat edges = new Mat();
            Cv2.Canny(gray, edges, 50, 150);

            LineSegmentPoint[] lines = Cv2.HoughLinesP(edges, 1, Math.PI / 180, 100, 100, 10);

            double angle = 0;
            foreach (LineSegmentPoint line in lines)
            {
                angle += Math.Atan2(line.P2.Y - line.P1.Y, line.P2.X - line.P1.X);
            }

            angle /= lines.Length;
            angle = angle * 180 / Math.PI;

            Mat rotationMatrix = Cv2.GetRotationMatrix2D(new Point2f(originalImage.Cols / 2, originalImage.Rows / 2), angle, 1);

            Mat rotatedImage = new Mat();
            Cv2.WarpAffine(originalImage, rotatedImage, rotationMatrix, originalImage.Size());

            return BitmapConverter.ToBitmap(rotatedImage);
        }
        private double GetAngle(Point2f point1, Point2f point2)
        {
            double dx = point2.X - point1.X;
            double dy = point2.Y - point1.Y;
            return Math.Atan2(dy, dx) * 180 / Math.PI;
        }
        private Point2f[] FindLargestContour(OpenCvSharp.Point[][] contours)
        {
            double maxArea = 0;
            Point2f[] largestContour = null;
            foreach (var contour in contours)
            {
                double area = Cv2.ContourArea(contour);
                if (area > maxArea)
                {
                    maxArea = area;
                    largestContour = Array.ConvertAll(contour, point => new Point2f(point.X, point.Y));
                }
            }
            return largestContour;
        }
        private Bitmap ResizeImage(Bitmap image, double widthFactor, double heightFactor)
        {
            int newWidth = (int)(image.Width * widthFactor);
            int newHeight = (int)(image.Height * heightFactor);

            Bitmap resizedImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return resizedImage;
        }

        private Bitmap CropImage(Bitmap image, double x, double y, double width, double height)
        {
            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle((int)x, (int)y, (int)width, (int)height);
            Bitmap croppedImage = image.Clone(cropRect, image.PixelFormat);
            return croppedImage;
        }

        private Bitmap HistogramEqualization(Bitmap image)
        {
            Bitmap grayscaleImage = ConvertToGrayscale(image);
            int[] histogram = CalculateHistogram(grayscaleImage);
            int[] cdf = CalculateCDF(histogram);
            int[] mapping = CalculateMapping(cdf, grayscaleImage.Width * grayscaleImage.Height);

            Bitmap equalizedImage = ApplyMapping(grayscaleImage, mapping);

            return equalizedImage;
        }
        private Bitmap ConvertToGrayscale(Bitmap image)
        {
            Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = image.GetPixel(x, y);
                    int average = (pixel.R + pixel.G + pixel.B) / 3;
                    grayscaleImage.SetPixel(x, y, System.Drawing.Color.FromArgb(average, average, average));
                }
            }

            return grayscaleImage;
        }

        private int[] CalculateHistogram(Bitmap image)
        {
            int[] histogram = new int[256];

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = image.GetPixel(x, y);
                    int intensity = pixel.R;
                    histogram[intensity]++;
                }
            }

            return histogram;
        }

        private int[] CalculateCDF(int[] histogram)
        {
            int[] cdf = new int[256];
            cdf[0] = histogram[0];

            for (int i = 1; i < 256; i++)
            {
                cdf[i] = cdf[i - 1] + histogram[i];
            }

            return cdf;
        }

        private int[] CalculateMapping(int[] cdf, int totalPixels)
        {
            int[] mapping = new int[256];

            for (int i = 0; i < 256; i++)
            {
                mapping[i] = (int)((double)cdf[i] / totalPixels * 255);
            }

            return mapping;
        }

        private Bitmap ApplyMapping(Bitmap image, int[] mapping)
        {
            Bitmap equalizedImage = new Bitmap(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    System.Drawing.Color pixel = image.GetPixel(x, y);
                    int intensity = pixel.R;
                    int equalizedIntensity = mapping[intensity];
                    equalizedImage.SetPixel(x, y, System.Drawing.Color.FromArgb(equalizedIntensity, equalizedIntensity, equalizedIntensity));
                }
            }

            return equalizedImage;
        }
        #endregion
        public async Task<Result<Bitmap>> DoPerspectiveTransformBitmapAsync(Bitmap inputBitmap)
        {
            try
            {
                if (inputBitmap == null)
                {
                    _logger.LogError("Invalid inputBitmap for perspective transformation.");
                    return Result.Failure<Bitmap>("PerspectiveTransformationService", "Invalid input inputBitmap for perspective transformation.");
                }
                return await Task.Run(() =>
                {
                    using (Mat img = BitmapConverter.ToMat(inputBitmap))
                    {
                        int hh = img.Height;
                        int ww = img.Width;

                        using (Mat template = img.Clone())
                        {
                            int ht = template.Height;
                            int wd = template.Width;

                            using (Mat grayImage = new Mat())
                            {
                                if (img.Channels() == 1)
                                {
                                    _logger.LogError("Input image is already grayscale.");
                                    return Result.Failure<Bitmap>("PerspectiveTransformationService", "Input image is already grayscale.");
                                }
                                Cv2.CvtColor(img, grayImage, ColorConversionCodes.BGR2GRAY);

                                Mat thresh = new Mat();
                                Cv2.Threshold(grayImage, thresh, 0, 255, ThresholdTypes.Binary | ThresholdTypes.Otsu);

                                Mat pad = new Mat();
                                Cv2.CopyMakeBorder(thresh, pad, 20, 20, 20, 20, BorderTypes.Constant, Scalar.Black);

                                Mat kernel = Cv2.GetStructuringElement(MorphShapes.Ellipse, new OpenCvSharp.Size(15, 15));
                                Mat morph = new Mat();
                                Cv2.MorphologyEx(pad, morph, MorphTypes.Close, kernel);

                                OpenCvSharp.Rect roi = new OpenCvSharp.Rect(20, 20, ww, hh);
                                Mat croppedMorph = new Mat(morph, roi);

                                OpenCvSharp.Point[][] contours;
                                HierarchyIndex[] hierarchy;
                                Cv2.FindContours(croppedMorph, out contours, out hierarchy, RetrievalModes.External, ContourApproximationModes.ApproxSimple);
                                var bigContour = FindLargestContour(contours);

                                double peri = Cv2.ArcLength(bigContour, true);
                                Point2f[] corners = Cv2.ApproxPolyDP(bigContour, 0.04 * peri, true);

                                Mat polygon = img.Clone();
                                OpenCvSharp.Point[] cornersInt = Array.ConvertAll(corners, point => new OpenCvSharp.Point((int)point.X, (int)point.Y));
                                Cv2.Polylines(polygon, new[] { cornersInt }, true, Scalar.Green, 2, LineTypes.AntiAlias);

                                double angle = GetAngle(corners[0], corners[1]);

                                Point2f[] oCorners;
                                if (angle >= -45 && angle < 45) // Horizontal
                                {
                                    bool isMirrored = corners[0].Y > corners[3].Y;
                                    if (isMirrored)
                                    {
                                        oCorners = new Point2f[] { new Point2f(wd, ht), new Point2f(wd, 0), new Point2f(0, 0), new Point2f(0, ht) };
                                    }
                                    else
                                    {
                                        oCorners = new Point2f[] { new Point2f(wd, ht), new Point2f(0, ht), new Point2f(0, 0), new Point2f(wd, 0) };
                                    }
                                }
                                else if (angle >= 45 && angle < 135) // Vertical
                                {
                                    bool isMirrored = corners[0].Y > corners[1].Y;

                                    if (isMirrored)
                                    {
                                        oCorners = new Point2f[] { new Point2f(wd, ht), new Point2f(wd, 0), new Point2f(0, 0), new Point2f(0, ht) };
                                    }
                                    else
                                    {
                                        oCorners = new Point2f[] { new Point2f(0, 0), new Point2f(0, ht), new Point2f(wd, ht), new Point2f(wd, 0) };
                                    }
                                }
                                else if (angle >= -135 && angle < -45) // Vertical (upside down)
                                {
                                    bool isMirrored = corners[2].Y > corners[3].Y;

                                    if (isMirrored)
                                    {
                                        oCorners = new Point2f[] { new Point2f(0, ht), new Point2f(0, 0), new Point2f(wd, 0), new Point2f(wd, ht) };
                                    }
                                    else
                                    {
                                        oCorners = new Point2f[] { new Point2f(wd, 0), new Point2f(wd, ht), new Point2f(0, ht), new Point2f(0, 0) };
                                    }
                                }
                                else // Upside down
                                {
                                    bool isMirrored = corners[0].Y > corners[3].Y;
                                    if (isMirrored)
                                    {
                                        oCorners = new Point2f[] { new Point2f(wd, ht), new Point2f(wd, 0), new Point2f(0, 0), new Point2f(0, ht) };
                                    }
                                    else
                                    {
                                        oCorners = new Point2f[] { new Point2f(0, 0), new Point2f(wd, 0), new Point2f(wd, ht), new Point2f(0, ht) };
                                        oCorners = new Point2f[] { new Point2f(wd, 0), new Point2f(0, 0), new Point2f(0, ht), new Point2f(wd, ht) };
                                    }

                                }
                                Mat M = Cv2.GetPerspectiveTransform(corners, oCorners);

                                if (M == null || M.Rows != 3 || M.Cols != 3 || M.Type() != MatType.CV_64FC1)
                                {
                                    _logger.LogError("Invalid perspective transformation matrix.");
                                    return Result.Failure<Bitmap>("PerspectiveTransformationService", "Invalid perspective transformation matrix.");
                                }
                                Mat warped = new Mat();
                                try
                                {
                                    Cv2.WarpPerspective(img, warped, M, new OpenCvSharp.Size(wd, ht), InterpolationFlags.Linear);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError("Error during perspective transformation: " + ex.Message);
                                    return Result.Failure<Bitmap>("PerspectiveTransformationService", "Error during perspective transformation: " + ex.Message);
                                }
                                Bitmap warpedImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(warped);
                                grayImage.Dispose();
                                warped.Dispose();

                                return warpedImage;
                               
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during perspective transformation: " + ex.Message);
                return Result.Failure<Bitmap>("PerspectiveTransformationService", "Error during perspective transformation: " + ex.Message);
            }
        }

        public async Task<Result<Bitmap>> ResizeImageAsync(Bitmap inputBitmap, int width, int height)
        {

            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                image.Mutate(x => x.Resize(width, height));
                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }

        public async Task<Result<Bitmap>> ConvertToGrayscaleAsync(Bitmap inputBitmap)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                image.Mutate(x => x.Grayscale());
                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }
        public async Task<Result<Bitmap>> ApplyBlurringAsync(Bitmap inputBitmap)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                image.Mutate(x => x.GaussianBlur());
                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }

        public async Task<Result<Bitmap>> ApplyEdgeDetectionAsync(Bitmap inputBitmap)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                image.Mutate(x => x.AutoOrient().DetectEdges());
                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }

        public async Task<Result<Bitmap>> ApplyHistogramEqualizationAsync(Bitmap inputBitmap)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                image.Mutate(x => x.AutoOrient().Grayscale().HistogramEqualization());
                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }

        public async Task<Result<Bitmap>> ApplyNormalizationAsync(Bitmap inputBitmap)
        {
            using (var image = SixLabors.ImageSharp.Image.Load(inputBitmapToByteArray(inputBitmap)))
            {
                //var histogram = image.Frames.RootFrame.GetHistogram();
                //var normalized = image.Clone(ctx => ctx.HistogramEqualization(histogram));

                using (var outputStream = new MemoryStream())
                {
                    await image.SaveAsJpegAsync(outputStream);//var normalized = image.Clone(ctx => ctx.HistogramEqualization(histogram)); // TODO
                    return Result.Success(new Bitmap(outputStream));
                }
            }
        }


    }
}
