using DocumentClassificationZonalOcr.Shared.Enums;

namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class ZoneDto
    {
        public int Id { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double ActualWidth { get; set; }
        public double ActualHeight { get; set; }
        public double ActualImageWidth { get; set; }
        public double ActualImageHeight { get; set; }
        public int FieldId { get; set; }
        public string Regex { get; set; }
        public string WhiteList { get; set; }
        public bool IsDuplicated { get; set; }
        public ZoneFieldType ZoneFieldType { get; set; }
        public int FormSampleId { get; set; }
        public bool IsAnchorPoint { get; set; }
        public string? AnchorPointPath { get; set; }
    }
}
