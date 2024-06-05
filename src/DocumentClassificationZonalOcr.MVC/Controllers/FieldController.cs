using DocumentClassificationZonalOcr.MVC.Clients;
using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Shared.Requests;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class FieldController : Controller
    {
        private readonly IFieldClient _fieldClient;
        private readonly IFormClient _formClient;

        public FieldController(IFieldClient fieldClient, IFormClient formClient)
        {
            _fieldClient = fieldClient;
            _formClient = formClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int formId)
        {
            return View(formId);
        }
        [HttpPost]
        public async Task<IActionResult> LoadData(int formId, [FromBody] DataTableOptionsDto parameters)
        {
            var response = await _formClient.GetAllFormFieldsAsync(formId, parameters);

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }

            var totalRecords = response.Data.TotalCount;
            var data = response.Data.Items.Select((group, index) => new
            {
                Serial = index + 1,
                Name = group.Name,
                Type = group.Type.ToString(),
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
        public async Task<IActionResult> AddEdit(int? id, int formId)
        {
            ViewData["FormId"] = formId;

            if (id.HasValue)
            {
                var response = await _fieldClient.GetFieldByIdAsync(id.Value);
                if (!response.Succeeded)
                {
                    return BadRequest(response.Message);
                }
                return View(response.Data);
            }
            return View(new FieldDto { FormId = formId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEdit(int formId, FieldDto fieldDto)
        {
            if (fieldDto.Id == 0)
            {
                var response = await _formClient.AddFieldToFormAsync(formId, new FieldRequestDto { Name = fieldDto.Name, Type = fieldDto.Type });
                if (!response.Succeeded)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["FormId"] = formId;
                    return View(fieldDto);
                }
            }
            else
            {
                var response = await _fieldClient.ModifyFieldAsync(fieldDto.Id, new FieldRequestDto { Name = fieldDto.Name, Type = fieldDto.Type });
                if (!response.Succeeded)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["FormId"] = formId;
                    return View(fieldDto);
                }
            }

            return RedirectToAction(nameof(Index), new { formId = formId });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _fieldClient.RemoveFieldAsync(id);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok();
        }


    }
}
