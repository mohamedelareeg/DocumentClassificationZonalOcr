using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Form>> GetByIdAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            return form != null ? Result.Success(form) : Result.Failure<Form>("Form not found.");
        }

        public async Task<Result<Form>> CreateAsync(Form form)
        {
            _context.Set<Form>().Add(form);
            await _context.SaveChangesAsync();
            return Result.Success(form);
        }

        public async Task<Result<bool>> UpdateAsync(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var form = await _context.Set<Form>().FindAsync(id);
            if (form == null)
                return Result.Failure<bool>("Form not found.");

            _context.Set<Form>().Remove(form);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
    }
}
