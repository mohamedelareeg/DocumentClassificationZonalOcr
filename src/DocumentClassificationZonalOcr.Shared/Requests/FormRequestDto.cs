using DocumentClassificationZonalOcr.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class FormRequestDto
    {
        public string Name { get; set; }
        public List<FormSampleRequestDto> Samples { get; set; }
        public List<FieldRequestDto> Fields { get; set; }
    }
}
