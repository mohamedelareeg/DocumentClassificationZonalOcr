using DocumentClassificationZonalOcr.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Dtos
{
    public class FieldDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public string Name { get; set; }
        public FieldType Type { get; set; }
    }
}
