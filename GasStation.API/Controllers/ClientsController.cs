using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clients = await _clientService.GetAllClientsAsync();

            if (clients == null || !clients.Any())
            {
                return NotFound("No clients found.");
            }

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clientService.AddClientAsync(clientDto);
                return CreatedAtAction(nameof(GetById), new { id = clientDto.ID_Client }, clientDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the client: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingClient = await _clientService.GetClientByIdAsync(id);

            if (existingClient == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }

            try
            {
                await _clientService.UpdateClientAsync(id, clientDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the client: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingClient = await _clientService.GetClientByIdAsync(id);

            if (existingClient == null)
            {
                return NotFound($"Client with ID {id} not found.");
            }

            try
            {
                await _clientService.DeleteClientAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the client: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                await _clientService.DeleteAllClientsAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting all clients: {ex.Message}");
            }
        }
    }
}
