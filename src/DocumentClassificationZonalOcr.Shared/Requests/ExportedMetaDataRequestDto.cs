using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class ExportedMetaDataRequestDto
    {
        public int FieldId { get; set; }
        public string Value { get; set; }
        public int PaperId { get; set; }
    }
}
