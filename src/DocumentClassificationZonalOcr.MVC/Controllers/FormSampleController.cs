using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
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

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var totalRecords = response.Data.TotalCount;
            var data = response.Data.Items.Select((group, index) => new
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
                if (!response.Succeeded)
                {
                    ModelState.AddModelError("", response.Message);
                }
               
            }

            return RedirectToAction("Index", new { formId = formId });
        }



        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _formSampleClient.RemoveFormSampleAsync(id);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> AddZones(int id)
        {
            var response = await _formSampleClient.GetFormSampleByIdAsync(id);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            response.Data.ImagePath = response.Data.ImagePath.Replace("\\", "/");


            return View(response.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddZones(int id, List<ZoneDto> zones)
        {
            return RedirectToAction("AddZones", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> SubmitRectangleData([FromBody] FormSampleZoneRequestDto data)
        {
            var deleteResponse = await _formSampleClient.RemoveAllZonesAsync(data.Id);
            if (!deleteResponse.Succeeded)
            {
                return BadRequest(deleteResponse.Message);
            }
            foreach (var item in data.Rectangles)
            {
                var response = await _formSampleClient.AddZoneAsync(data.Id,item);
                if (!response.Succeeded)
                {
                    return BadRequest(response.Message);
                }
            }
            //return RedirectToAction("Index", new { formId = formId });
            return Ok(data);
        }
    }
}
