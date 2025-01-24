using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class AdministratorService : IAdministratorService
    {
        private readonly ApplicationDbContext _context;

        public AdministratorService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить всех администраторов
        public async Task<IEnumerable<AdministratorDto>> GetAllAdministratorsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrators = await _context.Administrators
                    .Select(a => new AdministratorDto
                    {
                        ID_Administrator = a.ID_Administrator,
                        LastName = a.LastName,
                        FirstName = a.FirstName,
                        MiddleName = a.MiddleName,
                        Phone = a.Phone,
                        Email = a.Email,
                        Login = a.Login,
                        Password = a.Password
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return administrators;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Получить администратора по ID
        public async Task<AdministratorDto> GetAdministratorByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrator = await _context.Administrators.FindAsync(id);
                if (administrator == null) throw new KeyNotFoundException("Administrator not found");

                var administratorDto = new AdministratorDto
                {
                    ID_Administrator = administrator.ID_Administrator,
                    LastName = administrator.LastName,
                    FirstName = administrator.FirstName,
                    MiddleName = administrator.MiddleName,
                    Phone = administrator.Phone,
                    Email = administrator.Email,
                    Login = administrator.Login,
                    Password = administrator.Password
                };

                await transaction.CommitAsync();
                return administratorDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Добавить нового администратора
        public async Task AddAdministratorAsync(AdministratorDto administratorDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrator = new Administrator
                {
                    LastName = administratorDto.LastName,
                    FirstName = administratorDto.FirstName,
                    MiddleName = administratorDto.MiddleName,
                    Phone = administratorDto.Phone,
                    Email = administratorDto.Email,
                    Login = administratorDto.Login,
                    Password = administratorDto.Password
                };

                _context.Administrators.Add(administrator);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Обновить существующего администратора
        public async Task UpdateAdministratorAsync(int id, AdministratorDto administratorDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrator = await _context.Administrators.FindAsync(id);
                if (administrator == null) throw new KeyNotFoundException("Administrator not found");

                administrator.LastName = administratorDto.LastName;
                administrator.FirstName = administratorDto.FirstName;
                administrator.MiddleName = administratorDto.MiddleName;
                administrator.Phone = administratorDto.Phone;
                administrator.Email = administratorDto.Email;
                administrator.Login = administratorDto.Login;
                administrator.Password = administratorDto.Password; // Обновлять с хэшированием

                _context.Administrators.Update(administrator);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<AdministratorDto> GetAdministratorByLoginAsync(string login)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrator = await _context.Administrators.FirstOrDefaultAsync(c => c.Login == login);

                if (administrator == null) return null;

                var administratorDto = new AdministratorDto
                {
                    ID_Administrator = administrator.ID_Administrator,
                    LastName = administrator.LastName,
                    FirstName = administrator.FirstName,
                    MiddleName = administrator.MiddleName,
                    Phone = administrator.Phone,
                    Email = administrator.Email,
                    Login = administrator.Login,
                    Password = administrator.Password
                };

                await transaction.CommitAsync();
                return administratorDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Удалить администратора
        public async Task DeleteAdministratorAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var administrator = await _context.Administrators.FindAsync(id);
                if (administrator == null) throw new KeyNotFoundException("Administrator not found");

                _context.Administrators.Remove(administrator);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Удалить всех администраторов
        public async Task DeleteAllAdministratorsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Administrators.RemoveRange(_context.Administrators);
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
