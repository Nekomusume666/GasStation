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

            if (supplies == null || !supplies.Any())
            {
                return NotFound("No supplies found.");
            }

            return Ok(supplies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var supply = await _supplyService.GetSupplyByIdAsync(id);

            if (supply == null)
            {
                return NotFound($"Supply with ID {id} not found.");
            }

            return Ok(supply);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplyDto supplyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _supplyService.AddSupplyAsync(supplyDto);
                return CreatedAtAction(nameof(GetById), new { id = supplyDto.ID_Supply }, supplyDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the supply: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplyDto supplyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingSupply = await _supplyService.GetSupplyByIdAsync(id);

            if (existingSupply == null)
            {
                return NotFound($"Supply with ID {id} not found.");
            }

            try
            {
                await _supplyService.UpdateSupplyAsync(id, supplyDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the supply: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingSupply = await _supplyService.GetSupplyByIdAsync(id);

            if (existingSupply == null)
            {
                return NotFound($"Supply with ID {id} not found.");
            }

            try
            {
                await _supplyService.DeleteSupplyAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the supply: {ex.Message}");
            }
        }
    }
}
