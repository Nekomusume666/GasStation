using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministratorsController : ControllerBase
    {
        private readonly IAdministratorService _administratorService;

        public AdministratorsController(IAdministratorService administratorService)
        {
            _administratorService = administratorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var administrators = await _administratorService.GetAllAdministratorsAsync();

            if (administrators == null || !administrators.Any())
            {
                return NotFound("No administrators found.");
            }

            return Ok(administrators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var administrator = await _administratorService.GetAdministratorByIdAsync(id);

            if (administrator == null)
            {
                return NotFound($"Administrator with ID {id} not found.");
            }

            return Ok(administrator);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdministratorDto administratorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _administratorService.AddAdministratorAsync(administratorDto);
                return CreatedAtAction(nameof(GetById), new { id = administratorDto.ID_Administrator }, administratorDto);
            }
            catch (Exception ex)
            {
                // Логирование ошибки, если необходимо
                return StatusCode(500, $"An error occurred while creating the administrator: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdministratorDto administratorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAdministrator = await _administratorService.GetAdministratorByIdAsync(id);

            if (existingAdministrator == null)
            {
                return NotFound($"Administrator with ID {id} not found.");
            }

            try
            {
                await _administratorService.UpdateAdministratorAsync(id, administratorDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Логирование ошибки, если необходимо
                return StatusCode(500, $"An error occurred while updating the administrator: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingAdministrator = await _administratorService.GetAdministratorByIdAsync(id);

            if (existingAdministrator == null)
            {
                return NotFound($"Administrator with ID {id} not found.");
            }

            try
            {
                await _administratorService.DeleteAdministratorAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Логирование ошибки, если необходимо
                return StatusCode(500, $"An error occurred while deleting the administrator: {ex.Message}");
            }
        }
    }
}
