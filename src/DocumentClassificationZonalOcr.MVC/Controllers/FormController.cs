using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class FormController : Controller
    {
        private readonly IFormClient _formClient;

        public FormController(IFormClient formClient)
        {
            _formClient = formClient;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoadData([FromBody] DataTableOptionsDto parameters)
        {
            var response = await _formClient.GetAllFormsAsync(parameters);

            if (response == null)
            {
                return BadRequest("Error fetching data");
            }
            var totalRecords = response.TotalCount;
            var data = response.Items.Select((group, index) => new
            {
                Serial = index + 1,
                Name = group.Name,
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
        [HttpGet]
        public async Task<IActionResult> AddEdit(int? id)
        {
            if (id.HasValue)
            {
                var response = await _formClient.GetFormByIdAsync(id.Value);
                if (response == null)
                {
                    return NotFound();
                }
                return View(response);
            }
            return View(new FormDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(FormDto formDto)
        {
            
            if (formDto.Id == 0)
            {
                var response = await _formClient.CreateFormAsync(formDto.Name);
                if (response == null)
                {
                    ModelState.AddModelError("","Error creating form");
                    return View(formDto);
                }
            }
            else
            {
                var response = await _formClient.ModifyFormNameAsync(formDto.Id,formDto.Name);
                if (response == null)
                {
                    ModelState.AddModelError("", "Error updating form");
                    return View(formDto);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _formClient.RemoveFormAsync(id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
