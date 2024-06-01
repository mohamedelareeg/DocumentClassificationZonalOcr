using DocumentClassificationZonalOcr.Shared.Enums;

namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class FormDetectionSettingDto
    {
        public int Id { get; set; }
        public OcrEngine OcrEngine { get; set; }
        public decimal FormSimilarity { get; set; }
        public DetectOptions DetectOptions { get; set; }
        public double ZoneAllowedWidth { get; set; }
        public double ZoneAllowedHeight { get; set; }
        public DetectAlgorithm DetectAlgorithm { get; set; }
        public bool ActivePerspectiveTransform { get; set; }
        public bool ResizeImage { get; set; } = false;
        public bool ConvertToGrayscale { get; set; } = false;
        public bool Normalization { get; set; } = false;
        public bool Blurring { get; set; } = false;
        public bool EdgeDetection { get; set; } = false;
        public bool HistogramEqualization { get; set; } = false;
    }
}
