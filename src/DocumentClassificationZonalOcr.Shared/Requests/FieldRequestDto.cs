using DocumentClassificationZonalOcr.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class FieldRequestDto
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
    }
}
