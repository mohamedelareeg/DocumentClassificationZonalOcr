using DocumentClassificationZonalOcr.MVC.Clients;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class DetectionController : Controller
    {
        private readonly IPaperClient _paperClient;

        public DetectionController(IPaperClient paperClient)
        {
            _paperClient = paperClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/Detection/upload")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            try
            {
                var response = await _paperClient.ProcessImagesAsync(files);
                if (!response.Succeeded)
                {
                    return BadRequest(response.Message);
                }
                return Ok(new { Message = "Files uploaded successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error uploading files", Error = ex.Message });
            }
        }
    }
}
