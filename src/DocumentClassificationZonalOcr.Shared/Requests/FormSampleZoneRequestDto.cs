using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class FormSampleZoneRequestDto
    {
        public int Id { get; set; }
        public int FormId { get; set; }
        public List<ZoneRequestDto> Rectangles { get; set; }
    }
}
