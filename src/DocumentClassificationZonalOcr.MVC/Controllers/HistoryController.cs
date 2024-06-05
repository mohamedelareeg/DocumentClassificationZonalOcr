using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class HistoryController : Controller
    {
        private readonly IPaperClient _paperClient;

        public HistoryController(IPaperClient paperClient)
        {
            _paperClient = paperClient;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoadData([FromBody] DataTableOptionsDto parameters)
        {
            var response = await _paperClient.GetAllPapersAsync(parameters);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var totalRecords = response.Data.TotalCount;
            var data = response.Data.Items.Select((group, index) => new
            {
                Serial = index + 1,
                ImagePath = group.FilePath,
                Id = group.Id,
                FilterationParams = parameters
            }).ToList();

            return Json(new
            {
                draw = parameters.Draw,
                recordsTotal = totalRecords,
                recordsFiltered = totalRecords,
                data
            });
        }
        public async Task<IActionResult> GetDetails(int id, string[] fields = null)
        {
            var response = await _paperClient.GetAllPaperMetadataAsync(id);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            foreach (var item in response.Data.Items)
            {
                if (!string.IsNullOrEmpty(item.FilePath))
                {
                    item.FilePath = item.FilePath.Replace("\\", "/");
                }
            }
            //var detailsData = new List<object>();

            //detailsData.Add(new { fieldName = "Field 1", fieldValue = "Dummy Value 1", imagePath = "https://via.placeholder.com/100" });
            //detailsData.Add(new { fieldName = "Field 2", fieldValue = "Dummy Value 2", imagePath = "https://via.placeholder.com/120" });

            return Ok(response.Data.Items);
        }
    }
}
