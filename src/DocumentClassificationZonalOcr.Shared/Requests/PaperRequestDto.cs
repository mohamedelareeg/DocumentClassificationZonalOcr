using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentClassificationZonalOcr.Shared.Requests
{
    public class PaperRequestDto
    {
        public int FormId { get; set; }
        public int DocumentId { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
