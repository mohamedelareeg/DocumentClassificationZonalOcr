namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class FormDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FormSampleDto>? Samples { get; set; }
        public List<FieldDto>? Fields { get; set; }
    }
}
