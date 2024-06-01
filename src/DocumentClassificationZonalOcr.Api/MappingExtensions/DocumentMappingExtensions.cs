using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class DocumentMappingExtensions
    {
        public static DocumentDto ToDto(this Document document)
        {
            return new DocumentDto
            {
                Id = document.Id,
                Name = document.Name,
                FormId = document.FormId,
                Papers = document.Papers.Select(paper => new PaperDto
                {
                    Id = paper.Id,
                    Name = paper.Name,
                    FilePath = paper.FilePath,
                    DocumentId = paper.DocumentId ?? 0,
                    FormId = paper.FormId ?? 0
                }).ToList()
            };
        }
        public static Document ToEntity(this DocumentDto documentDto)
        {
            var result = Document.Create(documentDto.Name, documentDto.FormId);
            if (result.IsFailure)
            {
                throw new Exception(result.Error);
            }

            var document = result.Value;

            foreach (var paperDto in documentDto.Papers)
            {
                var paperResult = Paper.Create(paperDto.FilePath, paperDto.Name, document.Id, paperDto.FormId);
                if (paperResult.IsFailure)
                {
                    throw new Exception(paperResult.Error);
                }

                document.AddPaper(paperResult.Value);
            }

            return document;
        }
        public static Document ToEntity(this DocumentRequestDto documentRequestDto)
        {
            var documentResult = Document.Create(documentRequestDto.Name, documentRequestDto.FormId);
            if (documentResult.IsFailure)
            {
                throw new Exception(documentResult.Error);
            }

            var document = documentResult.Value;

            foreach (var paperDto in documentRequestDto.Papers)
            {
                var paperResult = Paper.Create(paperDto.FilePath, paperDto.Name, document.Id, document.FormId);
                if (paperResult.IsFailure)
                {
                    throw new Exception(paperResult.Error);
                }

                document.AddPaper(paperResult.Value);
            }

            return document;
        }
    }
}
