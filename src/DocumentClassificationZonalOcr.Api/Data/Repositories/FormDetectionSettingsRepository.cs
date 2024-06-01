using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Data.Repositories
{
    public class FormDetectionSettingsRepository : IFormDetectionSettingsRepository
    {
        private readonly ApplicationDbContext _context;

        public FormDetectionSettingsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Result<FormDetectionSetting> GetSettings()
        {
            var settings = _context.Set<FormDetectionSetting>().FirstOrDefault();
            if (settings == null)
                return Result.Failure<FormDetectionSetting>("FormDetectionSettingsRepository.GetSettings", "Settings not found.");

            return Result.Success(settings);
        }

        public async Task<Result<FormDetectionSetting>> GetSettingsAsync()
        {
            var settings = _context.Set<FormDetectionSetting>().FirstOrDefault();
            if (settings == null)
                return Result.Failure<FormDetectionSetting>("FormDetectionSettingsRepository.GetSettings", "Settings not found.");

            return Result.Success(settings);
        }

        public async Task<Result<bool>> UpdateSettingsAsync(FormDetectionSetting settings)
        {
            var existingSettings = _context.Set<FormDetectionSetting>().FirstOrDefault();

            if (existingSettings == null)
            {
                _context.Set<FormDetectionSetting>().Add(settings);
            }
            else
            {
                _context.Entry(existingSettings).CurrentValues.SetValues(settings);
            }

            _context.SaveChanges();

            return Result.Success(true);
        }

    }
}
