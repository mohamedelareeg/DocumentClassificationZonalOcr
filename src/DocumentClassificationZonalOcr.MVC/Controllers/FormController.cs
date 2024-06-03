using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

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

            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            var totalRecords = response.Data.TotalCount;
            var data = response.Data.Items.Select((group, index) => new
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
                if (!response.Succeeded)
                {
                    return BadRequest(response.Message);
                }
                return View(response.Data);
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
                if (!response.Succeeded)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(formDto);
                }
              
            }
            else
            {
                var response = await _formClient.ModifyFormNameAsync(formDto.Id, formDto.Name);
                if (!response.Succeeded)
                {
                    ModelState.AddModelError("", response.Message);
                    return View(formDto);
                }
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetFields(int formId)
        {
            //var dummyField = await _formClient.AddFieldToFormAsync(formId, new Shared.Requests.FieldRequestDto { Name = "Test", Type = Shared.Enums.FieldType.Text });
            var dummyDatatableOptions = new DataTableOptionsDto { Draw = 1, Start = 0, Length = 100 , OrderBy = "Name" , SearchText = "" };
            var response = await _formClient.GetAllFormFieldsAsync(formId, dummyDatatableOptions);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Json(response.Data.Items);
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _formClient.RemoveFormAsync(id);
            if (!response.Succeeded)
            {
                return BadRequest(response.Message);
            }
            return Ok();
        }

    }
}
