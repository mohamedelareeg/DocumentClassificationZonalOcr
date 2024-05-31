using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Document>> GetByIdAsync(int id)
        {
            var document = await _context.Set<Document>().FindAsync(id);
            return document != null ? Result.Success(document) : Result.Failure<Document>("Document not found.");
        }

        public async Task<Result<Document>> CreateAsync(Document document)
        {
            _context.Set<Document>().Add(document);
            await _context.SaveChangesAsync();
            return Result.Success(document);
        }

        public async Task<Result<bool>> UpdateAsync(Document document)
        {
            _context.Entry(document).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var document = await _context.Set<Document>().FindAsync(id);
            if (document == null)
                return Result.Failure<bool>("Document not found.");

            _context.Set<Document>().Remove(document);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
    }
}
