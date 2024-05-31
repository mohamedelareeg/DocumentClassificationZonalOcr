using System;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class Paper : BaseEntity
    {
        public string Name { get; private set; }
        public string FilePath { get; private set; }
        public int? DocumentId { get; private set; }
        public int? FormId { get; private set; }

        private Paper() { }

        private Paper(string name, string filePath, int? documentId, int? formId)
        {
            Name = name;
            FilePath = filePath;
            DocumentId = documentId;
            FormId = formId;
        }

        public static Result<Paper> Create(string filePath, string name = null, int? documentId = null, int? formId = null)
        {
            if (string.IsNullOrEmpty(filePath))
                return Result.Failure<Paper>("Create", "File path cannot be null or empty.");

            return Result.Success(new Paper(name, filePath, documentId, formId));
        }

        public Result<bool> ModifyDocumentId(int? documentId)
        {
            if (documentId <= 0)
                return Result.Failure<bool>("ModifyDocumentId", "Document ID must be greater than zero.");

            DocumentId = documentId;
            return Result.Success(true);
        }

        public Result<bool> ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<bool>("ModifyName", "Paper name cannot be null or empty.");

            Name = name;
            return Result.Success(true);
        }

        public Result<bool> ModifyFormId(int? formId)
        {
            if (formId.HasValue && formId <= 0)
                return Result.Failure<bool>("ModifyFormId", "Form ID must be greater than zero.");

            FormId = formId;
            return Result.Success(true);
        }

    }
}
