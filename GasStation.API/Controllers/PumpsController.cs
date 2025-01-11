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
            return Ok(pumps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var pump = await _pumpService.GetPumpByIdAsync(id);
            return Ok(pump);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PumpDto pumpDto)
        {
            await _pumpService.AddPumpAsync(pumpDto);
            return CreatedAtAction(nameof(GetById), new { id = pumpDto.ID_Pump }, pumpDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PumpDto pumpDto)
        {
            await _pumpService.UpdatePumpAsync(id, pumpDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _pumpService.DeletePumpAsync(id);
            return NoContent();
        }
    }
}
