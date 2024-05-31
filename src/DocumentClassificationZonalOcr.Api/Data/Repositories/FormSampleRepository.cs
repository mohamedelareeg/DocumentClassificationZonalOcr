using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class FormSampleRepository : IFormSampleRepository
    {
        private readonly ApplicationDbContext _context;

        public FormSampleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<FormSample>> GetByIdAsync(int id)
        {
            var formSample = await _context.Set<FormSample>().FindAsync(id);
            return formSample != null ? Result.Success(formSample) : Result.Failure<FormSample>("FormSample not found.");
        }

        public async Task<Result<FormSample>> CreateAsync(FormSample formSample)
        {
            _context.Set<FormSample>().Add(formSample);
            await _context.SaveChangesAsync();
            return Result.Success(formSample);
        }

        public async Task<Result<bool>> UpdateAsync(FormSample formSample)
        {
            _context.Entry(formSample).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var formSample = await _context.Set<FormSample>().FindAsync(id);
            if (formSample == null)
                return Result.Failure<bool>("FormSample not found.");

            _context.Set<FormSample>().Remove(formSample);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }
    }
}
