using DocumentClassificationZonalOcr.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class FormDetectionSettingRequestDto
    {
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
