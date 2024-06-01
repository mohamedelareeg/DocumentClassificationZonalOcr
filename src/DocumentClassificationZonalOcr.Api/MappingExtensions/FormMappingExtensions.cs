using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.SqlServer.Server;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class FormMappingExtensions
    {
        public static FormDto ToDto(this Form form)
        {
            var formDto = new FormDto
            {
                Id = form.Id,
                Name = form.Name
            };

            if (form.Samples != null)
            {
                formDto.Samples = form.Samples.Select(sample => new FormSampleDto
                {
                    Id = sample.Id,
                    ImagePath = sample.ImagePath,
                    Zones = sample.Zones.Select(zone => new ZoneDto
                    {
                        Id = zone.Id,
                        X = zone.X,
                        Y = zone.Y,
                        ActualWidth = zone.ActualWidth,
                        ActualHeight = zone.ActualHeight,
                        ActualImageWidth = zone.ActualImageWidth,
                        ActualImageHeight = zone.ActualImageHeight,
                        FieldId = (int)zone.FieldId,
                        Regex = zone.Regex,
                        WhiteList = zone.WhiteList,
                        IsDuplicated = zone.IsDuplicated,
                        ZoneFieldType = zone.ZoneFieldType,
                        FormSampleId = zone.FormSampleId,
                        IsAnchorPoint = zone.IsAnchorPoint,
                        AnchorPointPath = zone.AnchorPointPath
                    }).ToList()
                }).ToList();
            }

            if (form.Fields != null)
            {
                formDto.Fields = form.Fields.Select(field => new FieldDto
                {
                    Id = field.Id,
                    FormId = form.Id,
                    Name = field.Name,
                    Type = field.Type
                }).ToList();
            }

            return formDto;
        }

        public static Form ToEntity(this FormDto formDto)
        {
            var formResult = Form.Create(formDto.Name);
            if (formResult.IsFailure)
            {
                throw new Exception(formResult.Error);
            }
            var form = formResult.Value;
            foreach (var fieldDto in formDto.Fields)
            {
                var field = Field.Create(fieldDto.Name, fieldDto.Type, form.Id);
                if (field.IsFailure)
                {
                    throw new Exception(field.Error);
                }

                var addFieldResult = form.AddField(field.Value);
                if (addFieldResult.IsFailure)
                {
                    throw new Exception(addFieldResult.Error);
                }
            }

            foreach (var sampleDto in formDto.Samples)
            {
                var formSampleResult = FormSample.Create(form.Id, sampleDto.ImagePath);
                if (formSampleResult.IsFailure)
                {
                    throw new Exception(formSampleResult.Error);
                }
                var formSample = formSampleResult.Value;

                foreach (var zoneDto in sampleDto.Zones)
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

                var addFormSampleResult = form.AddSample(formSample);
                if (addFormSampleResult.IsFailure)
                {
                    throw new Exception(addFormSampleResult.Error);
                }
            }

            return form;
        }
        public static Form ToEntity(this FormRequestDto formRequestDto)
        {
            var formResult = Form.Create(formRequestDto.Name);
            if (formResult.IsFailure)
            {
                throw new Exception(formResult.Error);
            }
            var form = formResult.Value;

            foreach (var fieldDto in formRequestDto.Fields)
            {
                var field = Field.Create(fieldDto.Name, fieldDto.Type, form.Id);
                if (field.IsFailure)
                {
                    throw new Exception(field.Error);
                }

                var addFieldResult = form.AddField(field.Value);
                if (addFieldResult.IsFailure)
                {
                    throw new Exception(addFieldResult.Error);
                }
            }
            foreach (var sampleDto in formRequestDto.Samples)
            {
                var formSampleResult = FormSample.Create(form.Id, sampleDto.ImagePath);
                if (formSampleResult.IsFailure)
                {
                    throw new Exception(formSampleResult.Error);
                }
                var formSample = formSampleResult.Value;

                if (sampleDto.Zones != null)
                {
                    foreach (var zoneDto in sampleDto.Zones)
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

                var addFormSampleResult = form.AddSample(formSample);
                if (addFormSampleResult.IsFailure)
                {
                    throw new Exception(addFormSampleResult.Error);
                }
            }

            return form;
        }

    }
}
