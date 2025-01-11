using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound("No employees found.");
            }

            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _employeeService.AddEmployeeAsync(employeeDto);
                return CreatedAtAction(nameof(GetById), new { id = employeeDto.ID_Employee }, employeeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the employee: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            try
            {
                await _employeeService.UpdateEmployeeAsync(id, employeeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the employee: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEmployee = await _employeeService.GetEmployeeByIdAsync(id);

            if (existingEmployee == null)
            {
                return NotFound($"Employee with ID {id} not found.");
            }

            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the employee: {ex.Message}");
            }
        }
    }
}
