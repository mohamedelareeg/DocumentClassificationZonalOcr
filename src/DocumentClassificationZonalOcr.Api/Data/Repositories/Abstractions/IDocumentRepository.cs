using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IDocumentRepository
    {
        Task<Result<Document>> GetByIdAsync(int id);
        Task<Result<Document>> CreateAsync(Document document);
        Task<Result<bool>> UpdateAsync(Document document);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
