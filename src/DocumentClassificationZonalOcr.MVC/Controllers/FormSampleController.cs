using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class FormSampleController : Controller
    {
        private readonly IFormClient _formClient;
        private readonly IFormSampleClient _formSampleClient;


        public FormSampleController(IFormClient formClient, IFormSampleClient formSampleClient)
        {
            _formClient = formClient;
            _formSampleClient = formSampleClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int formId)
        {
            return View(formId);
        }

        [HttpPost]
        public async Task<IActionResult> LoadData(int formId, [FromBody] DataTableOptionsDto parameters)
        {
            var response = await _formClient.GetAllFormSamplesAsync(formId, parameters);

            if (response == null)
            {
                return BadRequest("Error fetching data");
            }

            var totalRecords = response.TotalCount;
            var data = response.Items.Select((group, index) => new
            {
                Serial = index + 1,
                ImagePath = group.ImagePath,
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

        [HttpPost]
        public async Task<IActionResult> UploadImage(int formId, IFormFile imageFile)
        {
            if (imageFile != null)
            {
                var response = await _formClient.CreateFormSampleAsync(formId, imageFile);
                if (response == null)
                {
                    ModelState.AddModelError("", "Error creating form Sample");
                }
            }

            return RedirectToAction("Index", new { formId = formId });
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _formSampleClient.RemoveFormSampleAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AddZones(int id)
        {
            var formSample = await _formSampleClient.GetFormSampleByIdAsync(id);
            if (formSample == null)
            {
                return NotFound();
            }
            formSample.ImagePath = formSample.ImagePath.Replace("\\", "/");


            return View(formSample);
        }

        [HttpPost]
        public async Task<IActionResult> AddZones(int id, List<ZoneDto> zones)
        {
            return RedirectToAction("AddZones", new { id = id });
        }
    }
}
