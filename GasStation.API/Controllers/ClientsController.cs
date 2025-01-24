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

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] ClientDto clientDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clientService.AddClientAsync(clientDto);
                return Ok(new { Message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the client: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Login) || string.IsNullOrEmpty(loginDto.Password))
            {
                return BadRequest("Login and Password are required.");
            }

            var client = await _clientService.GetClientByLoginAsync(loginDto.Login);

            if (client == null || client.Password != loginDto.Password) // Здесь можно хэшировать и сравнивать пароли
            {
                return Unauthorized("Invalid login or password.");
            }

            // Можно вернуть токен, если используется аутентификация через JWT
            return Ok(client);
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
