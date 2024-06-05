using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class ExportedMetaDataDto
    {
        public int Id { get; set; }
        public int FieldId { get; set; }
        public string Value { get; set; }
        public int PaperId { get; set; }
        public string FilePath { get; set; }
    }
}
