using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaperController : ControllerBase
    {
        private readonly IPaperService _paperService;

        public PaperController(IPaperService paperService)
        {
            _paperService = paperService;
        }

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<Result<IEnumerable<Paper>>>> GetAllPapersByFormIdAsync(int formId)
        {
            var result = await _paperService.GetAllPapersByFormIdAsync(formId);
            return Ok(result);
        }

        [HttpGet("document/{documentId}")]
        public async Task<ActionResult<Result<IEnumerable<Paper>>>> GetAllPapersByDocumentIdAsync(int documentId)
        {
            var result = await _paperService.GetAllPapersByDocumentIdAsync(documentId);
            return Ok(result);
        }

        [HttpGet("metadata/{paperId}")]
        public async Task<ActionResult<Result<IEnumerable<ExportedMetaData>>>> GetAllPaperMetadataAsync(int paperId)
        {
            var result = await _paperService.GetAllPaperMetadataAsync(paperId);
            return Ok(result);
        }

        [HttpPost("process-image")]
        public async Task<ActionResult<Result<bool>>> ProcessImageAsync(IFormFile image)
        {
            var result = await _paperService.ProcessImageAsync(image);
            return Ok(result);
        }

        [HttpPost("process-images")]
        public async Task<ActionResult<Result<bool>>> ProcessImagesAsync(List<IFormFile> images)
        {
            var result = await _paperService.ProcessImagesAsync(images);
            return Ok(result);
        }
    }
}
