using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;

namespace DocumentClassificationZonalOcr.Api.MappingExtensions
{
    public static class FieldMappingExtensions
    {
        public static FieldDto ToDto(this Field field)
        {
            return new FieldDto
            {
                Id = field.Id,
                FormId = field.FormId,
                Name = field.Name,
                Type = field.Type
            };
        }

        public static Field ToEntity(this FieldDto fieldDto)
        {
            var field = Field.Create(fieldDto.Name, fieldDto.Type, fieldDto.FormId);
            if (field.IsFailure)
            {
                throw new Exception(field.Error);
            }

            return field.Value;
        }
        public static Field ToEntity(this FieldRequestDto fieldRequestDto, int formId)
        {
            var fieldResult = Field.Create(fieldRequestDto.Name, fieldRequestDto.Type, formId);
            if (fieldResult.IsFailure)
            {
                throw new Exception(fieldResult.Error);
            }

            return fieldResult.Value;
        }
    }
}
