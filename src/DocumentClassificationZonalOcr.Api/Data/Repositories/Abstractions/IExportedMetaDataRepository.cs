using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions
{
    public interface IExportedMetaDataRepository
    {
        Task<Result<bool>> AddMetadataAsync(ExportedMetaData metaData);
        Task<Result<IEnumerable<ExportedMetaData>>> GetAllByFieldIdAsync(int fieldId);
        Task<Result<IEnumerable<ExportedMetaData>>> GetAllByPaperIdAsync(int paperId);
    }
}
