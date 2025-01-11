using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PumpsController : ControllerBase
    {
        private readonly IPumpService _pumpService;

        public PumpsController(IPumpService pumpService)
        {
            _pumpService = pumpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pumps = await _pumpService.GetAllPumpsAsync();

            if (pumps == null || !pumps.Any())
            {
                return NotFound("No pumps found.");
            }

            return Ok(pumps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pump = await _pumpService.GetPumpByIdAsync(id);

            if (pump == null)
            {
                return NotFound($"Pump with ID {id} not found.");
            }

            return Ok(pump);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PumpDto pumpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _pumpService.AddPumpAsync(pumpDto);
                return CreatedAtAction(nameof(GetById), new { id = pumpDto.ID_Pump }, pumpDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the pump: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PumpDto pumpDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPump = await _pumpService.GetPumpByIdAsync(id);

            if (existingPump == null)
            {
                return NotFound($"Pump with ID {id} not found.");
            }

            try
            {
                await _pumpService.UpdatePumpAsync(id, pumpDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the pump: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingPump = await _pumpService.GetPumpByIdAsync(id);

            if (existingPump == null)
            {
                return NotFound($"Pump with ID {id} not found.");
            }

            try
            {
                await _pumpService.DeletePumpAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the pump: {ex.Message}");
            }
        }
    }
}
