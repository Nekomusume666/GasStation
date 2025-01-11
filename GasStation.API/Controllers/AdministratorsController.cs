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
            return Ok(administrators);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var administrator = await _administratorService.GetAdministratorByIdAsync(id);
            return Ok(administrator);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdministratorDto administratorDto)
        {
            await _administratorService.AddAdministratorAsync(administratorDto);
            return CreatedAtAction(nameof(GetById), new { id = administratorDto.ID_Administrator }, administratorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AdministratorDto administratorDto)
        {
            await _administratorService.UpdateAdministratorAsync(id, administratorDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _administratorService.DeleteAdministratorAsync(id);
            return NoContent();
        }
    }
}
