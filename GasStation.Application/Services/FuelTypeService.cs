using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class FuelTypeService : IFuelTypeService
    {
        private readonly ApplicationDbContext _context;

        public FuelTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuelTypeDto>> GetAllFuelTypesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuelTypes = await _context.FuelTypes
                    .Select(ft => new FuelTypeDto
                    {
                        ID_FuelType = ft.ID_FuelType,
                        Type = ft.Type
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return fuelTypes;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<FuelTypeDto> GetFuelTypeByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuelType = await _context.FuelTypes.FindAsync(id);
                if (fuelType == null) throw new KeyNotFoundException("Fuel type not found");

                var fuelTypeDto = new FuelTypeDto
                {
                    ID_FuelType = fuelType.ID_FuelType,
                    Type = fuelType.Type
                };

                await transaction.CommitAsync();
                return fuelTypeDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddFuelTypeAsync(FuelTypeDto fuelTypeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuelType = new FuelType
                {
                    Type = fuelTypeDto.Type
                };

                _context.FuelTypes.Add(fuelType);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateFuelTypeAsync(int id, FuelTypeDto fuelTypeDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuelType = await _context.FuelTypes.FindAsync(id);
                if (fuelType == null) throw new KeyNotFoundException("Fuel type not found");

                fuelType.Type = fuelTypeDto.Type;

                _context.FuelTypes.Update(fuelType);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteFuelTypeAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuelType = await _context.FuelTypes.FindAsync(id);
                if (fuelType == null) throw new KeyNotFoundException("Fuel type not found");

                _context.FuelTypes.Remove(fuelType);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllFuelTypesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.FuelTypes.RemoveRange(_context.FuelTypes);
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
