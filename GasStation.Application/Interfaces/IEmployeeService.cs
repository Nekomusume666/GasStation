using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(EmployeeDto employeeDto);
        Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto);
        Task DeleteEmployeeAsync(int id);
        Task DeleteAllEmployeesAsync();
    }
}
