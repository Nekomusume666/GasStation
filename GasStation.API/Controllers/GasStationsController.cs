using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GasStationsController : ControllerBase
    {
        private readonly IGasStationService _gasStationService;

        public GasStationsController(IGasStationService gasStationService)
        {
            _gasStationService = gasStationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gasStations = await _gasStationService.GetAllGasStationsAsync();
            return Ok(gasStations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gasStation = await _gasStationService.GetGasStationByIdAsync(id);
            return Ok(gasStation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GasStationDto gasStationDto)
        {
            await _gasStationService.AddGasStationAsync(gasStationDto);
            return CreatedAtAction(nameof(GetById), new { id = gasStationDto.ID_GasStation }, gasStationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GasStationDto gasStationDto)
        {
            await _gasStationService.UpdateGasStationAsync(id, gasStationDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gasStationService.DeleteGasStationAsync(id);
            return NoContent();
        }
    }
}
