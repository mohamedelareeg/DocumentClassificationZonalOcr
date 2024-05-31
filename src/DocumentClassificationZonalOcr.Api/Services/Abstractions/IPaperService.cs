using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IPaperService
    {
        Task<Result<IEnumerable<Paper>>> GetAllPapersByFormIdAsync(int formId);
        Task<Result<IEnumerable<Paper>>> GetAllPapersByDocumentIdAsync(int documentId);
        Task<Result<IEnumerable<ExportedMetaData>>> GetAllPaperMetadataAsync(int paperId);
        Task<Result<bool>> ProcessImageAsync(IFormFile image);
        Task<Result<bool>> ProcessImagesAsync(List<IFormFile> images);
    }
}
