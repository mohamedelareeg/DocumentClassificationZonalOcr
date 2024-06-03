using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IPaperRepository
    {
        Task<Result<Paper>> GetByIdAsync(int id);
        Task<Result<Paper>> CreateAsync(Paper paper);
        Task<Result<bool>> UpdateAsync(Paper paper);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<IEnumerable<Paper>>> GetAllByFormIdAsync(int formId);
        Task<Result<IEnumerable<Paper>>> GetAllByDocumentIdAsync(int documentId);
        Task<Result<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options);
    }
}
