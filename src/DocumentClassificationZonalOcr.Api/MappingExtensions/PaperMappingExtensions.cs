using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class PaperMappingExtensions
    {
      
        public static PaperDto ToDto(this Paper paper, string baseUrl)
        {
            return new PaperDto
            {
                Id = paper.Id,
                Name = paper.Name,
                FilePath = $"{baseUrl}/{paper.FilePath}",
                DocumentId = paper.DocumentId ?? 0,
                FormId = paper.FormId ?? 0
            };
        }
        public static Paper ToEntity(this PaperDto paperDto)
        {
            var paperResult = Paper.Create(paperDto.FilePath, paperDto.Name, paperDto.DocumentId, paperDto.FormId);
            if (paperResult.IsFailure)
            {
                throw new Exception(paperResult.Error);
            }

            return paperResult.Value;
        }
        public static Paper ToEntity(this PaperRequestDto paperRequestDto)
        {
            var paperResult = Paper.Create(paperRequestDto.FilePath, paperRequestDto.Name, null, paperRequestDto.FormId);
            if (paperResult.IsFailure)
            {
                throw new Exception(paperResult.Error);
            }

            return paperResult.Value;
        }
    }
}
