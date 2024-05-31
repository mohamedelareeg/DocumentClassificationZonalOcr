using DocumentClassificationZonalOcr.Api.Enums;
using DocumentClassificationZonalOcr.Api.Results;
using System;

namespace DocumentClassificationZonalOcr.Api.Models
{
    public class Field : BaseEntity
    {
        public string Name { get; private set; }
        public FieldType Type { get; private set; }
        public int FormId { get; private set; }

        private Field() { }

        private Field(string name, FieldType type, int formId)
        {
            Name = name;
            Type = type;
            FormId = formId;
        }

        public static Result<Field> Create(string name, FieldType type, int formId)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<Field>("Fields.Create", "Field name is required.");

            var field = new Field(name, type, formId);
            return Result.Success(field);
        }

        public Result<bool> ModifyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return Result.Failure<bool>("Field.ModifyName", "Field name cannot be null or empty.");

            Name = name;
            return Result.Success(true);
        }

        public Result<bool> ModifyType(FieldType type)
        {
            Type = type;
            return Result.Success(true);
        }
    }
}
