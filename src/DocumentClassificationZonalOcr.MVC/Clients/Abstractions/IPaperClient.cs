using DocumentClassificationZonalOcr.MVC.Base;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.MVC.Clients.Abstractions
{
    public interface IPaperClient
    {
        Task<BaseResponse<bool>> AddPaperToFormAsync(int formId, PaperRequestDto paper);
        Task<BaseResponse<PaperDto>> GetPaperByIdAsync(int paperId);
        Task<BaseResponse<List<PaperDto>>> GetAllPapersByFormIdAsync(int formId);
        Task<BaseResponse<List<PaperDto>>> GetAllPapersByDocumentIdAsync(int documentId);
        Task<BaseResponse<ExportedMetaDataDto>> GetAllPaperMetadataAsync(int paperId);
        Task<BaseResponse<bool>> ProcessImageAsync(IFormFile image);
        Task<BaseResponse<bool>> ProcessImagesAsync(List<IFormFile> images);
        Task<BaseResponse<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options);
    }
}
