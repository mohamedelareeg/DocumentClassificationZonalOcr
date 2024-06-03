using CaptureSolution.AutomaticReleaseApi.Controllers.Base;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class PaperController : AppControllerBase
    {
        private readonly IPaperService _paperService;

        public PaperController(IPaperService paperService)
        {
            _paperService = paperService;
        }

        [HttpGet("form/{formId}")]
        public async Task<IActionResult> GetAllPapersByFormIdAsync(int formId)
        {
            var result = await _paperService.GetAllPapersByFormIdAsync(formId);
            return CustomResult(result);
        }

        [HttpGet("document/{documentId}")]
        public async Task<IActionResult> GetAllPapersByDocumentIdAsync(int documentId)
        {
            var result = await _paperService.GetAllPapersByDocumentIdAsync(documentId);
            return CustomResult(result);
        }

        [HttpGet("metadata/{paperId}")]
        public async Task<IActionResult> GetAllPaperMetadataAsync(int paperId)
        {
            var result = await _paperService.GetAllPaperMetadataAsync(paperId);
            return CustomResult(result);
        }

        [HttpPost("process-image")]
        public async Task<IActionResult> ProcessImageAsync(IFormFile image)
        {
            var result = await _paperService.ProcessImageAsync(image);
            return CustomResult(result);
        }

        [HttpPost("process-images")]
        public async Task<IActionResult> ProcessImagesAsync(List<IFormFile> images)
        {
            var result = await _paperService.ProcessImagesAsync(images);
            return CustomResult(result);
        }
    }
}
