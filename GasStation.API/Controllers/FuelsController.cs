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
            return Ok(fuels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fuel = await _fuelService.GetFuelByIdAsync(id);
            return Ok(fuel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FuelDto fuelDto)
        {
            await _fuelService.AddFuelAsync(fuelDto);
            return CreatedAtAction(nameof(GetById), new { id = fuelDto.ID_Fuel }, fuelDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FuelDto fuelDto)
        {
            await _fuelService.UpdateFuelAsync(id, fuelDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _fuelService.DeleteFuelAsync(id);
            return NoContent();
        }
    }
}
