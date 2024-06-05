using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class ExportedMetaDataMappingExtensions
    {
        public static ExportedMetaDataDto ToDto(this ExportedMetaData exportedMetaData)
        {
            return new ExportedMetaDataDto
            {
                Id = exportedMetaData.Id,
                FieldId = exportedMetaData.FieldId,
                Value = exportedMetaData.Value,
                PaperId = exportedMetaData.PaperId,
                FilePath = exportedMetaData.ImagePath,
            };
        }
        public static OcrValuePreviewDto ToZoneDto(this ExportedMetaData zone, string baseUrl)
        {
            return new OcrValuePreviewDto
            {
                FieldId = zone.FieldId,
                FieldName = "Testing",
                FieldValue = zone.Value,
                FilePath = $"{baseUrl}/{zone.ImagePath}"
            };
        }

        public static ExportedMetaData ToEntity(this ExportedMetaDataDto exportedMetaDataDto)
        {
            var exportedMetaDataResult = ExportedMetaData.Create(exportedMetaDataDto.FieldId, exportedMetaDataDto.Value, exportedMetaDataDto.PaperId,exportedMetaDataDto.FilePath);
            if (exportedMetaDataResult.IsFailure)
            {
                throw new Exception(exportedMetaDataResult.Error);
            }

            return exportedMetaDataResult.Value;
        }

        public static ExportedMetaData ToEntity(this ExportedMetaDataRequestDto exportedMetaDataRequestDto)
        {
            var exportedMetaDataResult = ExportedMetaData.Create(exportedMetaDataRequestDto.FieldId, exportedMetaDataRequestDto.Value, exportedMetaDataRequestDto.PaperId, exportedMetaDataRequestDto.FilePath);
            if (exportedMetaDataResult.IsFailure)
            {
                throw new Exception(exportedMetaDataResult.Error);
            }

            return exportedMetaDataResult.Value;
        }
    }
}
