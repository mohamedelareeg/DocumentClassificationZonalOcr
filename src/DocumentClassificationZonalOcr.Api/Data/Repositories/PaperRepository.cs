using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class PaperRepository : IPaperRepository
    {
        private readonly ApplicationDbContext _context;

        public PaperRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Paper>> GetByIdAsync(int id)
        {
            var paper = await _context.Set<Paper>().FindAsync(id);
            return paper != null ? Result.Success(paper) : Result.Failure<Paper>("Paper not found.");
        }

        public async Task<Result<Paper>> CreateAsync(Paper paper)
        {
            _context.Set<Paper>().Add(paper);
            await _context.SaveChangesAsync();
            return Result.Success(paper);
        }

        public async Task<Result<bool>> UpdateAsync(Paper paper)
        {
            _context.Entry(paper).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var paper = await _context.Set<Paper>().FindAsync(id);
            if (paper == null)
                return Result.Failure<bool>("Paper not found.");

            _context.Set<Paper>().Remove(paper);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
        public async Task<Result<IEnumerable<Paper>>> GetAllByFormIdAsync(int formId)
        {
            return await _context.Set<Paper>().Where(p => p.FormId == formId).ToListAsync();
        }

        public async Task<Result<IEnumerable<Paper>>> GetAllByDocumentIdAsync(int documentId)
        {
            return await _context.Set<Paper>().Where(p => p.DocumentId == documentId).ToListAsync();
        }
    }
}
