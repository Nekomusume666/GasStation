using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        private readonly IFuelTypeService _fuelTypeService;

        public FuelTypesController(IFuelTypeService fuelTypeService)
        {
            _fuelTypeService = fuelTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var fuelTypes = await _fuelTypeService.GetAllFuelTypesAsync();

            if (fuelTypes == null || !fuelTypes.Any())
            {
                return NotFound("No fuel types found.");
            }

            return Ok(fuelTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);

            if (fuelType == null)
            {
                return NotFound($"Fuel type with ID {id} not found.");
            }

            return Ok(fuelType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuelTypeDto fuelTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _fuelTypeService.AddFuelTypeAsync(fuelTypeDto);
                return CreatedAtAction(nameof(GetById), new { id = fuelTypeDto.ID_FuelType }, fuelTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the fuel type: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuelTypeDto fuelTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingFuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);

            if (existingFuelType == null)
            {
                return NotFound($"Fuel type with ID {id} not found.");
            }

            try
            {
                await _fuelTypeService.UpdateFuelTypeAsync(id, fuelTypeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the fuel type: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingFuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);

            if (existingFuelType == null)
            {
                return NotFound($"Fuel type with ID {id} not found.");
            }

            try
            {
                await _fuelTypeService.DeleteFuelTypeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the fuel type: {ex.Message}");
            }
        }
    }
}
