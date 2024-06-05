using DocumentClassificationZonalOcr.MVC.Clients.Abstractions;
using DocumentClassificationZonalOcr.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.MVC.Controllers
{
    public class FormDetectionSettingsController : Controller
    {
        private readonly IFormDetectionSettingClient _formDetectionSettingClient;

        public FormDetectionSettingsController(IFormDetectionSettingClient formDetectionSettingClient)
        {
            _formDetectionSettingClient = formDetectionSettingClient;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _formDetectionSettingClient.GetSettingsAsync();
            if (!response.Succeeded)
            {
                return View("Error");
            }

            var settings = response.Data;
            return View(settings);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(FormDetectionSettingDto settings)
        {
            if (!ModelState.IsValid)
            {
                return View(settings);
            }

            var response = await _formDetectionSettingClient.UpdateSettingsAsync(settings);
            if (!response.Succeeded)
            {
                return View("Error");
            }

            TempData["SuccessMessage"] = "Settings updated successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}
