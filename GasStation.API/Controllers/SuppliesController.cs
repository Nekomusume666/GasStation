using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliesController : ControllerBase
    {
        private readonly ISupplyService _supplyService;

        public SuppliesController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var supplies = await _supplyService.GetAllSuppliesAsync();
            return Ok(supplies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supply = await _supplyService.GetSupplyByIdAsync(id);
            return Ok(supply);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplyDto supplyDto)
        {
            await _supplyService.AddSupplyAsync(supplyDto);
            return CreatedAtAction(nameof(GetById), new { id = supplyDto.ID_Supply }, supplyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplyDto supplyDto)
        {
            await _supplyService.UpdateSupplyAsync(id, supplyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _supplyService.DeleteSupplyAsync(id);
            return NoContent();
        }
    }
}
