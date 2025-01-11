using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employees = await _context.Employees
                    .Select(e => new EmployeeDto
                    {
                        ID_Employee = e.ID_Employee,
                        LastName = e.LastName,
                        FirstName = e.FirstName,
                        MiddleName = e.MiddleName,
                        Phone = e.Phone,
                        Email = e.Email,
                        ID_GasStation = e.ID_GasStation
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return employees;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<EmployeeDto> GetEmployeeByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) throw new KeyNotFoundException("Employee not found");

                var employeeDto = new EmployeeDto
                {
                    ID_Employee = employee.ID_Employee,
                    LastName = employee.LastName,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    Phone = employee.Phone,
                    Email = employee.Email,
                    ID_GasStation = employee.ID_GasStation
                };

                await transaction.CommitAsync();
                return employeeDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddEmployeeAsync(EmployeeDto employeeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = new Employee
                {
                    LastName = employeeDto.LastName,
                    FirstName = employeeDto.FirstName,
                    MiddleName = employeeDto.MiddleName,
                    Phone = employeeDto.Phone,
                    Email = employeeDto.Email,
                    ID_GasStation = employeeDto.ID_GasStation
                };

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) throw new KeyNotFoundException("Employee not found");

                employee.LastName = employeeDto.LastName;
                employee.FirstName = employeeDto.FirstName;
                employee.MiddleName = employeeDto.MiddleName;
                employee.Phone = employeeDto.Phone;
                employee.Email = employeeDto.Email;
                employee.ID_GasStation = employeeDto.ID_GasStation;

                _context.Employees.Update(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee == null) throw new KeyNotFoundException("Employee not found");

                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllEmployeesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Employees.RemoveRange(_context.Employees);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
