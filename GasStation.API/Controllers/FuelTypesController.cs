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
            return Ok(fuelTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fuelType = await _fuelTypeService.GetFuelTypeByIdAsync(id);
            return Ok(fuelType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuelTypeDto fuelTypeDto)
        {
            await _fuelTypeService.AddFuelTypeAsync(fuelTypeDto);
            return CreatedAtAction(nameof(GetById), new { id = fuelTypeDto.ID_FuelType }, fuelTypeDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuelTypeDto fuelTypeDto)
        {
            await _fuelTypeService.UpdateFuelTypeAsync(id, fuelTypeDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fuelTypeService.DeleteFuelTypeAsync(id);
            return NoContent();
        }
    }
}
