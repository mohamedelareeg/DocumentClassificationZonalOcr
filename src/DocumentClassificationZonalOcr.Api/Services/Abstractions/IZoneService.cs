using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;

namespace DocumentClassificationZonalOcr.Api.Services.Abstractions
{
    public interface IZoneService
    {
        Task<Result<bool>> ModifyZonePropertiesAsync(int zoneId, ZoneDto zoneDto);
        Task<Result<bool>> DeleteZoneAsync(int zoneId);
        Task<Result<Zone>> GetZoneByIdAsync(int zoneId);
    }
}
