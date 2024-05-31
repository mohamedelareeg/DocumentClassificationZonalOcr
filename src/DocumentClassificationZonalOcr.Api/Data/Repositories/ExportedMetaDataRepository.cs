using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class ExportedMetaDataRepository : IExportedMetaDataRepository
    {
        private readonly ApplicationDbContext _context;

        public ExportedMetaDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<bool>> AddMetadataAsync(ExportedMetaData metaData)
        {

            await _context.Set<ExportedMetaData>().AddAsync(metaData);
            await _context.SaveChangesAsync();
            return Result.Success(true);

        }

        public async Task<Result<IEnumerable<ExportedMetaData>>> GetAllByFieldIdAsync(int fieldId)
        {
            var metaDataList = await _context.Set<ExportedMetaData>()
                .Where(m => m.FieldId == fieldId)
                .ToListAsync();


            return Result.Success<IEnumerable<ExportedMetaData>>(metaDataList);
        }

        public async Task<Result<IEnumerable<ExportedMetaData>>> GetAllByPaperIdAsync(int paperId)
        {
            var metaDataList = await _context.Set<ExportedMetaData>()
                .Where(m => m.PaperId == paperId)
                .ToListAsync();

            return Result.Success<IEnumerable<ExportedMetaData>>(metaDataList);
        }
    }
}
