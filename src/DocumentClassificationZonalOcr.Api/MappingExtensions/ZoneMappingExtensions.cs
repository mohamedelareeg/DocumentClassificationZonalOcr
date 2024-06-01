using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class ZoneMappingExtensions
    {
        public static ZoneDto ToDto(this Zone zone)
        {
            return new ZoneDto
            {
                Id = zone.Id,
                X = zone.X,
                Y = zone.Y,
                ActualWidth = zone.ActualWidth,
                ActualHeight = zone.ActualHeight,
                ActualImageWidth = zone.ActualImageWidth,
                ActualImageHeight = zone.ActualImageHeight,
                FieldId = zone.FieldId ?? 0, 
                Regex = zone.Regex,
                WhiteList = zone.WhiteList,
                IsDuplicated = zone.IsDuplicated,
                ZoneFieldType = zone.ZoneFieldType,
                FormSampleId = zone.FormSampleId,
                IsAnchorPoint = zone.IsAnchorPoint,
                AnchorPointPath = zone.AnchorPointPath
            };
        }

        public static Zone ToEntity(this ZoneDto zoneDto)
        {
            var zone = Zone.Create(
                zoneDto.X,
                zoneDto.Y,
                zoneDto.ActualWidth,
                zoneDto.ActualHeight,
                zoneDto.ActualImageWidth,
                zoneDto.ActualImageHeight,
                zoneDto.FormSampleId,
                zoneDto.FieldId,
                zoneDto.Regex,
                zoneDto.WhiteList,
                zoneDto.IsDuplicated,
                zoneDto.ZoneFieldType,
                zoneDto.IsAnchorPoint,
                zoneDto.AnchorPointPath);
            if (zone.IsFailure)
            {
                throw new Exception(zone.Error);
            }

            return zone.Value;
        }
        public static Result<Zone> ToEntity(this ZoneRequestDto zoneRequestDto, int formSampleId)
        {
            var zone = Zone.Create(
                zoneRequestDto.X,
                zoneRequestDto.Y,
                zoneRequestDto.ActualWidth,
                zoneRequestDto.ActualHeight,
                zoneRequestDto.ActualImageWidth,
                zoneRequestDto.ActualImageHeight,
                formSampleId,
                zoneRequestDto.FieldId,
                zoneRequestDto.Regex,
                zoneRequestDto.WhiteList,
                zoneRequestDto.IsDuplicated,
                zoneRequestDto.ZoneFieldType,
                zoneRequestDto.IsAnchorPoint,
                zoneRequestDto.AnchorPointPath);

            if (zone.IsFailure)
            {
                throw new Exception(zone.Error);
            }

            return zone;
        }
    }
}
