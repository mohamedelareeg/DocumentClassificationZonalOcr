using DocumentClassificationZonalOcr.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class FormSampleRequestDto
    {
        public string ImagePath { get; set; }
        public List<ZoneRequestDto>? Zones { get; set; }
    }
}
