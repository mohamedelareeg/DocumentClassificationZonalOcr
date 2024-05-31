using DocumentClassificationZonalOcr.Api.Dtos;
using DocumentClassificationZonalOcr.Api.Models;
using DocumentClassificationZonalOcr.Api.Results;
using DocumentClassificationZonalOcr.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DocumentClassificationZonalOcr.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZoneController : ControllerBase
    {
        private readonly IZoneService _zoneService;

        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpPut("{zoneId}")]
        public async Task<ActionResult<Result<bool>>> ModifyZonePropertiesAsync(int zoneId, [FromBody] ZoneDto zoneDto)
        {
            var result = await _zoneService.ModifyZonePropertiesAsync(zoneId, zoneDto);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{zoneId}")]
        public async Task<ActionResult<Result<bool>>> DeleteZoneAsync(int zoneId)
        {
            var result = await _zoneService.DeleteZoneAsync(zoneId);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{zoneId}")]
        public async Task<ActionResult<Result<Zone>>> GetZoneByIdAsync(int zoneId)
        {
            var result = await _zoneService.GetZoneByIdAsync(zoneId);
            if (result.IsFailure)
            {
                return NotFound(result.Error);
            }

            return Ok(result.Value);
        }
    }
}
