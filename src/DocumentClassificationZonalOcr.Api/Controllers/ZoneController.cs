using DocumentClassificationZonalOcr.Shared.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using CaptureSolution.AutomaticReleaseApi.Controllers.Base;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [Route("api/[controller]")]
    public class ZoneController : AppControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpPut("{zoneId}")]
        public async Task<IActionResult> ModifyZonePropertiesAsync(int zoneId, [FromBody] ZoneDto zoneDto)
        {
            var result = await _zoneService.ModifyZonePropertiesAsync(zoneId, zoneDto);
            return CustomResult(result);
        }

        [HttpDelete("{zoneId}")]
        public async Task<IActionResult> DeleteZoneAsync(int zoneId)
        {
            var result = await _zoneService.DeleteZoneAsync(zoneId);
            return CustomResult(result);
        }

        [HttpGet("{zoneId}")]
        public async Task<IActionResult> GetZoneByIdAsync(int zoneId)
        {
            var result = await _zoneService.GetZoneByIdAsync(zoneId);
            return CustomResult(result);
        }
    }
}
