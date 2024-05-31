using DocumentClassificationZonalOcr.Api.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class Document : BaseEntity
    {
        public string Name { get; private set; }
        public int FormId { get; private set; }
        public List<Paper> Papers { get; private set; }

        private Document() { }

        private Document(string name, int formId)
        {
            Name = name;
            FormId = formId;
            Papers = new List<Paper>();
        }

        public static Result<Document> Create(string name, int formId)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Document>("Documents.Create", "Document name is required.");

            if (formId <= 0)
                return Result.Failure<Document>("Documents.Create", "Form ID must be greater than zero.");

            var document = new Document(name, formId);
            return Result.Success(document);
        }

        public Result<bool> ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<bool>("Document.ModifyName", "Document name cannot be null or empty.");

            Name = name;
            return Result.Success(true);
        }

        public Result<bool> AddPaper(Paper paper)
        {
            if (paper == null)
                return Result.Failure<bool>("Document.AddPaper", "Paper cannot be null.");

            Papers.Add(paper);
            return Result.Success(true);
        }

        public Result<bool> RemovePaper(int paperId)
        {
            var paperToRemove = Papers.FirstOrDefault(p => p.Id == paperId);
            if (paperToRemove == null)
                return Result.Failure<bool>("Document.RemovePaper", "Paper not found.");

            Papers.Remove(paperToRemove);
            return Result.Success(true);
        }
    }
}
