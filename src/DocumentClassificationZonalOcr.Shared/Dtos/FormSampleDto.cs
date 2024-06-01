namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class FormSampleDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string ImagePath { get; set; }
        public List<ZoneDto> Zones { get; set; }
    }
}
