using DocumentClassificationZonalOcr.Shared.Enums;
using DocumentClassificationZonalOcr.Api.Results;
using System;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class FormDetectionSetting : BaseEntity
    {
        public OcrEngine OcrEngine { get; private set; }
        public decimal FormSimilarity { get; private set; }
        public DetectOptions DetectOptions { get; private set; }
        public double ZoneAllowedWidth { get; private set; }
        public double ZoneAllowedHeight { get; private set; }
        public DetectAlgorithm DetectAlgorithm { get; private set; }
        public bool ActivePerspectiveTransform { get; private set; }
        public bool ResizeImage { get; private set; } = false;
        public bool ConvertToGrayscale { get; private set; } = false;
        public bool Normalization { get; private set; } = false;
        public bool Blurring { get; private set; } = false;
        public bool EdgeDetection { get; private set; } = false;
        public bool HistogramEqualization { get; private set; } = false;

        private FormDetectionSetting() { }

        private FormDetectionSetting(
            OcrEngine ocrEngine = OcrEngine.TesseractOcr,
            decimal formSimilarity = 5,
            DetectOptions detectOptions = DetectOptions.FullImage,
            double zoneAllowedWidth = 0,
            double zoneAllowedHeight = 0,
            DetectAlgorithm detectAlgorithm = DetectAlgorithm.SIFT,
            bool activePerspectiveTransform = false,
            bool resizeImage = false,
            bool convertToGrayscale = false,
            bool normalization = false,
            bool blurring = false,
            bool edgeDetection = false,
            bool histogramEqualization = false)
        {
            OcrEngine = ocrEngine;
            FormSimilarity = formSimilarity;
            DetectOptions = detectOptions;
            ZoneAllowedWidth = zoneAllowedWidth;
            ZoneAllowedHeight = zoneAllowedHeight;
            DetectAlgorithm = detectAlgorithm;
            ActivePerspectiveTransform = activePerspectiveTransform;
            ResizeImage = resizeImage;
            ConvertToGrayscale = convertToGrayscale;
            Normalization = normalization;
            Blurring = blurring;
            EdgeDetection = edgeDetection;
            HistogramEqualization = histogramEqualization;
        }

        public static Result<FormDetectionSetting> Create(
            OcrEngine ocrEngine = OcrEngine.TesseractOcr,
            decimal formSimilarity = 5,
            DetectOptions detectOptions = DetectOptions.FullImage,
            double zoneAllowedWidth = 0,
            double zoneAllowedHeight =0,
            DetectAlgorithm detectAlgorithm = DetectAlgorithm.SIFT,
            bool activePerspectiveTransform = false,
            bool resizeImage = false,
            bool convertToGrayscale = false,
            bool normalization = false,
            bool blurring = false,
            bool edgeDetection = false,
            bool histogramEqualization = false)
        {
            if (formSimilarity < 0 || formSimilarity > 100)
                return Result.Failure<FormDetectionSetting>("FormDetectionSettings.Create", "Form similarity must be between 0 and 100.");

            if (zoneAllowedWidth < 0)
                return Result.Failure<FormDetectionSetting>("FormDetectionSettings.Create", "Zone allowed width must be non-negative.");

            if (zoneAllowedHeight < 0)
                return Result.Failure<FormDetectionSetting>("FormDetectionSettings.Create", "Zone allowed height must be non-negative.");

            var settings = new FormDetectionSetting(
                ocrEngine,
                formSimilarity,
                detectOptions,
                zoneAllowedWidth,
                zoneAllowedHeight,
                detectAlgorithm,
                activePerspectiveTransform,
                resizeImage,
                convertToGrayscale,
                normalization,
                blurring,
                edgeDetection,
                histogramEqualization);
            return Result.Success(settings);
        }
        public Result<bool> Modify(
          OcrEngine ocrEngine = OcrEngine.TesseractOcr,
            decimal formSimilarity = 5,
            DetectOptions detectOptions = DetectOptions.FullImage,
            double zoneAllowedWidth = 0,
            double zoneAllowedHeight = 0,
            DetectAlgorithm detectAlgorithm = DetectAlgorithm.SIFT,
            bool activePerspectiveTransform = false,
            bool resizeImage = false,
            bool convertToGrayscale = false,
            bool normalization = false,
            bool blurring = false,
            bool edgeDetection = false,
            bool histogramEqualization = false)
        {
            if (formSimilarity < 0 || formSimilarity > 100)
                return Result.Failure<bool>("FormDetectionSettings.ModifyAllSettings", "Form similarity must be between 0 and 100.");

            if (zoneAllowedWidth < 0)
                return Result.Failure<bool>("FormDetectionSettings.ModifyAllSettings", "Zone allowed width must be non-negative.");

            if (zoneAllowedHeight < 0)
                return Result.Failure<bool>("FormDetectionSettings.ModifyAllSettings", "Zone allowed height must be non-negative.");

            OcrEngine = ocrEngine;
            FormSimilarity = formSimilarity;
            DetectOptions = detectOptions;
            ZoneAllowedWidth = zoneAllowedWidth;
            ZoneAllowedHeight = zoneAllowedHeight;
            DetectAlgorithm = detectAlgorithm;
            ActivePerspectiveTransform = activePerspectiveTransform;
            ResizeImage = resizeImage;
            ConvertToGrayscale = convertToGrayscale;
            Normalization = normalization;
            Blurring = blurring;
            EdgeDetection = edgeDetection;
            HistogramEqualization = histogramEqualization;

            return Result.Success(true);
        }

        public Result<bool> ModifyActivePerspectiveTransform(bool activePerspectiveTransform)
        {
            ActivePerspectiveTransform = activePerspectiveTransform;
            return Result.Success(true);
        }

        public Result<bool> ModifyOcrEngine(OcrEngine ocrEngine)
        {
            OcrEngine = ocrEngine;
            return Result.Success(true);
        }

        public Result<bool> ModifyFormSimilarity(decimal formSimilarity)
        {
            if (formSimilarity < 0 || formSimilarity > 100)
                return Result.Failure<bool>("FormDetectionSettings.ModifyFormSimilarity", "Form similarity must be between 0 and 100.");

            FormSimilarity = formSimilarity;
            return Result.Success(true);
        }

        public Result<bool> ModifyDetectOptions(DetectOptions detectOptions)
        {
            DetectOptions = detectOptions;
            return Result.Success(true);
        }

        public Result<bool> ModifyZoneAllowedWidth(double zoneAllowedWidth)
        {
            if (zoneAllowedWidth < 0)
                return Result.Failure<bool>("FormDetectionSettings.ModifyZoneAllowedWidth", "Zone allowed width must be non-negative.");

            ZoneAllowedWidth = zoneAllowedWidth;
            return Result.Success(true);
        }

        public Result<bool> ModifyZoneAllowedHeight(double zoneAllowedHeight)
        {
            if (zoneAllowedHeight < 0)
                return Result.Failure<bool>("FormDetectionSettings.ModifyZoneAllowedHeight", "Zone allowed height must be non-negative.");

            ZoneAllowedHeight = zoneAllowedHeight;
            return Result.Success(true);
        }

        public Result<bool> ModifyDetectAlgorithm(DetectAlgorithm detectAlgorithm)
        {
            DetectAlgorithm = detectAlgorithm;
            return Result.Success(true);
        }

        public Result<bool> ModifyResizeImage(bool resizeImage)
        {
            ResizeImage = resizeImage;
            return Result.Success(true);
        }

        public Result<bool> ModifyConvertToGrayscale(bool convertToGrayscale)
        {
            ConvertToGrayscale = convertToGrayscale;
            return Result.Success(true);
        }

        public Result<bool> ModifyNormalization(bool normalization)
        {
            Normalization = normalization;
            return Result.Success(true);
        }

        public Result<bool> ModifyBlurring(bool blurring)
        {
            Blurring = blurring;
            return Result.Success(true);
        }

        public Result<bool> ModifyEdgeDetection(bool edgeDetection)
        {
            EdgeDetection = edgeDetection;
            return Result.Success(true);
        }

        public Result<bool> ModifyHistogramEqualization(bool histogramEqualization)
        {
            HistogramEqualization = histogramEqualization;
            return Result.Success(true);
        }
    }
}
