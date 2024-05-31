using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class ExportedMetaData : BaseEntity
    {
        public int FieldId { get; private set; }
        public string Value { get; private set; }
        public int PaperId { get; private set; }

        private ExportedMetaData() { }

        private ExportedMetaData(int fieldId, string value, int paperId)
        {
            FieldId = fieldId;
            Value = value;
            PaperId = paperId;
        }

        public static Result<ExportedMetaData> Create(int fieldId, string value, int paperId)
        {
            if (fieldId <= 0)
                return Result.Failure<ExportedMetaData>("Create", "Field ID must be greater than zero.");

            if (string.IsNullOrEmpty(value))
                return Result.Failure<ExportedMetaData>("Create", "Value cannot be null or empty.");

            if (paperId <= 0)
                return Result.Failure<ExportedMetaData>("Create", "Paper ID must be greater than zero.");

            return Result.Success(new ExportedMetaData(fieldId, value, paperId));
        }

    }
}
