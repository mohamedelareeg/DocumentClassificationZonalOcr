using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class FormSampleMappingExtensions
    {
        public static FormSampleDto ToDto(this FormSample formSample, string baseUrl)
        {
            return new FormSampleDto
            {
                Id = formSample.Id,
                FormId = formSample.FormId,
                ImagePath = $"{baseUrl}/{formSample.ImagePath}",
                Zones = formSample.Zones?.Select(zone => new ZoneDto
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
                    AnchorPointPath = zone.AnchorPointPath,
                }).ToList() ?? new List<ZoneDto>()
            };
        }

        public static FormSample ToEntity(this FormSampleDto formSampleDto)
        {
            var formSampleResult = FormSample.Create(formSampleDto.FormId, formSampleDto.ImagePath);
            if (formSampleResult.IsFailure)
            {
                throw new Exception(formSampleResult.Error);
            }
            var formSample = formSampleResult.Value;

            foreach (var zoneDto in formSampleDto.Zones)
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

                var addZoneResult = formSample.AddZone(zone.Value);
                if (addZoneResult.IsFailure)
                {
                    throw new Exception(addZoneResult.Error);
                }
            }

            return formSample;
        }
        public static FormSample ToEntity(this FormSampleRequestDto formSampleRequestDto, int formId)
        {
            var formSampleResult = FormSample.Create(formId, formSampleRequestDto.ImagePath);
            if (formSampleResult.IsFailure)
            {
                throw new Exception(formSampleResult.Error);
            }

            var formSample = formSampleResult.Value;

            if (formSampleRequestDto.Zones != null)
            {
                foreach (var zoneDto in formSampleRequestDto.Zones)
                {
                    var zone = Zone.Create(
                        zoneDto.X,
                        zoneDto.Y,
                        zoneDto.ActualWidth,
                        zoneDto.ActualHeight,
                        zoneDto.ActualImageWidth,
                        zoneDto.ActualImageHeight,
                        formSample.Id,
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

                    var addZoneResult = formSample.AddZone(zone.Value);
                    if (addZoneResult.IsFailure)
                    {
                        throw new Exception(addZoneResult.Error);
                    }
                }
            }

            return formSample;
        }
    }
}

