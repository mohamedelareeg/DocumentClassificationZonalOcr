using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class FormSample : BaseEntity
    {
        public string ImagePath { get; private set; }
        public int FormId { get; private set; }
        public List<Zone>? Zones { get; private set; }

        private FormSample() { }

        private FormSample(int formId, string imagePath)
        {
            FormId = formId;
            ImagePath = imagePath;
            Zones = new List<Zone>();
        }

        public static Result<FormSample> Create(int formId, string imagePath)
        {
            if (formId <= 0)
                return Result.Failure<FormSample>("FormSamples.Create", "Form ID must be greater than zero.");

            if (string.IsNullOrEmpty(imagePath))
                return Result.Failure<FormSample>("FormSamples.Create", "Image path is required.");

            var formSample = new FormSample(formId, imagePath);
            return Result.Success(formSample);
        }

        public Result<FormSample> ModifyImagePath(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return Result.Failure<FormSample>("FormSample.ModifyImagePath", "Image path cannot be null or empty.");

            ImagePath = imagePath;
            return Result.Success(this);
        }

        public Result<bool> RemoveZone(int zoneId)
        {
            var zoneToRemove = Zones.FirstOrDefault(z => z.Id == zoneId);
            if (zoneToRemove == null)
                return Result.Failure<bool>("FormSample.RemoveZone", "Zone not found.");

            Zones.Remove(zoneToRemove);
            return Result.Success(true);
        }

        public Result<bool> AddZone(Zone zone)
        {
            if (zone == null)
                return Result.Failure<bool>("FormSample.AddZone", "Zone cannot be null.");

            Zones.Add(zone);
            return Result.Success(true);
        }
    }
}
