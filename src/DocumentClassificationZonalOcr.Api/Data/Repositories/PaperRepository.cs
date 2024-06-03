using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.MappingExtensions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class PaperRepository : IPaperRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PaperRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<Result<CustomList<PaperDto>>> GetAllPapersAsync(DataTableOptionsDto options)
        {
            var query = _context.Set<Paper>().AsQueryable();

            if (!string.IsNullOrEmpty(options.SearchText))
            {
                query = query.Where(f => f.Name.Contains(options.SearchText));
            }

            //if (!string.IsNullOrEmpty(options.OrderBy))
            //{
            //    query = ApplyOrderBy(query, options.OrderBy);
            //}

            int totalCount = await query.CountAsync();

            int totalPages = totalCount > 0 ? (int)Math.Ceiling((double)totalCount / options.Length) : 0;

            var papers = await query
                .Skip(options.Start)
                .Take(options.Length)
                .ToListAsync();

            var request = _httpContextAccessor.HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host.Value}";

            var paperDtos = papers.Select(paper => paper.ToDto(baseUrl));


            return Result.Success(paperDtos.ToCustomList(totalCount, totalPages));
        }
    }
}
