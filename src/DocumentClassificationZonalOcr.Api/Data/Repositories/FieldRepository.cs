using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly ApplicationDbContext _context;

        public FieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Field>> GetByIdAsync(int id)
        {
            var field = await _context.Set<Field>().FindAsync(id);
            return field != null ? Result.Success(field) : Result.Failure<Field>("Field not found.");
        }

        public async Task<Result<Field>> CreateAsync(Field field)
        {
            _context.Set<Field>().Add(field);
            await _context.SaveChangesAsync();
            return Result.Success(field);
        }

        public async Task<Result<bool>> UpdateAsync(Field field)
        {
            _context.Entry(field).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var field = await _context.Set<Field>().FindAsync(id);
            if (field == null)
                return Result.Failure<bool>("Field not found.");

            _context.Set<Field>().Remove(field);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
    }
}
