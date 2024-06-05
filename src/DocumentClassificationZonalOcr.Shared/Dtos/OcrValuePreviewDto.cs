using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class OcrValuePreviewDto
    {
        public int? FieldId { get; set; }
        public string? FieldName { get; set; }
        public string? FieldValue { get; set; }
        public string? FilePath { get; set; }
    }
}
