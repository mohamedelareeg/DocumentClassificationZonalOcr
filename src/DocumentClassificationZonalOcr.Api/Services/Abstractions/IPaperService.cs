using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IPaperService
    {
        Task<Result<IEnumerable<Paper>>> GetAllPapersByFormIdAsync(int formId);
        Task<Result<IEnumerable<Paper>>> GetAllPapersByDocumentIdAsync(int documentId);
        Task<Result<List<OcrValuePreviewDto>>> GetAllPaperMetadataAsync(int paperId);
        Task<Result<bool>> ProcessImageAsync(IFormFile image);
        Task<Result<bool>> ProcessImagesAsync(List<IFormFile> images);
        Task<Result<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options);
    }
}
