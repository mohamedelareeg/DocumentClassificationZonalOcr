using DocumentClassificationZonalOcr.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class DocumentRequestDto
    {
        public string Name { get; set; }
        public int FormId { get; set; }
        public List<PaperRequestDto> Papers { get; set; }
    }
}
