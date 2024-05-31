namespace DocumentClassificationZonalOcr.Api.Dtos
{
    public class FormSampleDto
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public List<ZoneDto> Zones { get; set; }
    }
}
