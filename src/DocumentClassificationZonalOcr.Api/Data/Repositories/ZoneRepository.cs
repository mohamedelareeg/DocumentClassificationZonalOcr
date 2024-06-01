using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.EntityFrameworkCore;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class ZoneRepository : IZoneRepository
    {
        private readonly ApplicationDbContext _context;

        public ZoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Zone>> GetByIdAsync(int id)
        {
            var zone = await _context.Set<Zone>().FindAsync(id);
            return zone != null ? Result.Success(zone) : Result.Failure<Zone>("Zone not found.");
        }

        public async Task<Result<Zone>> CreateAsync(Zone zone)
        {
            _context.Set<Zone>().Add(zone);
            await _context.SaveChangesAsync();
            return Result.Success(zone);
        }

        public async Task<Result<bool>> UpdateAsync(Zone zone)
        {
            _context.Entry(zone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var zone = await _context.Set<Zone>().FindAsync(id);
            if (zone == null)
                return Result.Failure<bool>("Zone not found.");

            _context.Set<Zone>().Remove(zone);
            await _context.SaveChangesAsync();
            return Result.Success(true);
        }

        public async Task<Result<IEnumerable<Zone>>> GetAllByFormSampleIdAsync(int formSampleId)
        {
            var zones = await _context.Set<Zone>().Where(z => z.FormSampleId == formSampleId).ToListAsync();
            return Result.Success<IEnumerable<Zone>>(zones);
        }
        public async Task<Result<IEnumerable<Zone>>> GetAllAnchorPointsAsync()
        {
            var anchorPoints = await _context.Set<Zone>().Where(z => z.IsAnchorPoint).ToListAsync();

            if (anchorPoints == null || !anchorPoints.Any())
                return Result.Failure<IEnumerable<Zone>>("ZoneRepository.GetAllAnchorPointsAsync", "No anchor points found.");

            return Result.Success<IEnumerable<Zone>>(anchorPoints);
        }
    }
}
