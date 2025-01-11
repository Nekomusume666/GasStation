using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelsController : ControllerBase
    {
        private readonly IFuelService _fuelService;

        public FuelsController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fuels = await _fuelService.GetAllFuelsAsync();

            if (fuels == null || !fuels.Any())
            {
                return NotFound("No fuels found.");
            }

            return Ok(fuels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fuel = await _fuelService.GetFuelByIdAsync(id);

            if (fuel == null)
            {
                return NotFound($"Fuel with ID {id} not found.");
            }

            return Ok(fuel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuelDto fuelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _fuelService.AddFuelAsync(fuelDto);
                return CreatedAtAction(nameof(GetById), new { id = fuelDto.ID_Fuel }, fuelDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the fuel: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuelDto fuelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFuel = await _fuelService.GetFuelByIdAsync(id);

            if (existingFuel == null)
            {
                return NotFound($"Fuel with ID {id} not found.");
            }

            try
            {
                await _fuelService.UpdateFuelAsync(id, fuelDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the fuel: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingFuel = await _fuelService.GetFuelByIdAsync(id);

            if (existingFuel == null)
            {
                return NotFound($"Fuel with ID {id} not found.");
            }

            try
            {
                await _fuelService.DeleteFuelAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the fuel: {ex.Message}");
            }
        }
    }
}
