using DocumentClassificationZonalOcr.Api.Data.Repositories;
using DocumentClassificationZonalOcr.Api.Data.Repositories.Abstractions;
using DocumentClassificationZonalOcr.Api.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.SqlServer.Server;

namespace DocumentClassificationZonalOcr.Api.Services
{
    public class ZoneService : IZoneService
    {
        private readonly IZoneRepository _zoneRepository;

        public ZoneService(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        public async Task<Result<bool>> ModifyZonePropertiesAsync(int zoneId, ZoneDto zoneDto)
        {
            var zoneResult = await _zoneRepository.GetByIdAsync(zoneId);
            if (zoneResult.IsFailure)
                return Result.Failure<bool>("ModifyZonePropertiesAsync", zoneResult.Error);

            var modifyResult = zoneResult.Value.ModifyZoneProperties(zoneDto.X, zoneDto.Y, zoneDto.ActualWidth,
                zoneDto.ActualHeight, zoneDto.ActualImageWidth, zoneDto.ActualImageHeight, zoneDto.FieldId,
                zoneDto.Regex, zoneDto.WhiteList, zoneDto.IsDuplicated, zoneDto.ZoneFieldType, zoneDto.IsAnchorPoint);

            if (modifyResult.IsFailure)
                return Result.Failure<bool>("ModifyZonePropertiesAsync", modifyResult.Error);

            return await _zoneRepository.UpdateAsync(zoneResult.Value);
        }

        public async Task<Result<bool>> DeleteZoneAsync(int zoneId)
        {
            var result = await _zoneRepository.DeleteAsync(zoneId);
            if (result.IsFailure)
                return Result.Failure<bool>(result.Error);

            return Result.Success(true);
        }

        public async Task<Result<Zone>> GetZoneByIdAsync(int zoneId)
        {
            var result = await _zoneRepository.GetByIdAsync(zoneId);
            if (result.IsFailure)
                return Result.Failure<Zone>(result.Error);

            return result.Value;
        }
    }
}
