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
    }
}
