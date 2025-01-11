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

            if (gasStations == null || !gasStations.Any())
            {
                return NotFound("No gas stations found.");
            }

            return Ok(gasStations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var gasStation = await _gasStationService.GetGasStationByIdAsync(id);

            if (gasStation == null)
            {
                return NotFound($"Gas station with ID {id} not found.");
            }

            return Ok(gasStation);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GasStationDto gasStationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _gasStationService.AddGasStationAsync(gasStationDto);
                return CreatedAtAction(nameof(GetById), new { id = gasStationDto.ID_GasStation }, gasStationDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the gas station: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GasStationDto gasStationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingGasStation = await _gasStationService.GetGasStationByIdAsync(id);

            if (existingGasStation == null)
            {
                return NotFound($"Gas station with ID {id} not found.");
            }

            try
            {
                await _gasStationService.UpdateGasStationAsync(id, gasStationDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the gas station: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingGasStation = await _gasStationService.GetGasStationByIdAsync(id);

            if (existingGasStation == null)
            {
                return NotFound($"Gas station with ID {id} not found.");
            }

            try
            {
                await _gasStationService.DeleteGasStationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the gas station: {ex.Message}");
            }
        }
    }
}
