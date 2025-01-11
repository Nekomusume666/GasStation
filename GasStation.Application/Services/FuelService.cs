using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class FuelService : IFuelService
    {
        private readonly ApplicationDbContext _context;

        public FuelService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FuelDto>> GetAllFuelsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuels = await _context.Fuels
                    .Select(f => new FuelDto
                    {
                        ID_Fuel = f.ID_Fuel,
                        PricePerLiter = f.PricePerLiter,
                        Quantity = f.Quantity,
                        ID_GasStation = f.ID_GasStation,
                        ID_FuelType = f.ID_FuelType
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return fuels;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<FuelDto> GetFuelByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuel = await _context.Fuels.FindAsync(id);
                if (fuel == null) throw new KeyNotFoundException("Fuel not found");

                var fuelDto = new FuelDto
                {
                    ID_Fuel = fuel.ID_Fuel,
                    PricePerLiter = fuel.PricePerLiter,
                    Quantity = fuel.Quantity,
                    ID_GasStation = fuel.ID_GasStation,
                    ID_FuelType = fuel.ID_FuelType
                };

                await transaction.CommitAsync();
                return fuelDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddFuelAsync(FuelDto fuelDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuel = new Fuel
                {
                    PricePerLiter = fuelDto.PricePerLiter,
                    Quantity = fuelDto.Quantity,
                    ID_GasStation = fuelDto.ID_GasStation,
                    ID_FuelType = fuelDto.ID_FuelType
                };

                _context.Fuels.Add(fuel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateFuelAsync(int id, FuelDto fuelDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuel = await _context.Fuels.FindAsync(id);
                if (fuel == null) throw new KeyNotFoundException("Fuel not found");

                fuel.PricePerLiter = fuelDto.PricePerLiter;
                fuel.Quantity = fuelDto.Quantity;
                fuel.ID_GasStation = fuelDto.ID_GasStation;
                fuel.ID_FuelType = fuelDto.ID_FuelType;

                _context.Fuels.Update(fuel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteFuelAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var fuel = await _context.Fuels.FindAsync(id);
                if (fuel == null) throw new KeyNotFoundException("Fuel not found");

                _context.Fuels.Remove(fuel);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllFuelsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Fuels.RemoveRange(_context.Fuels);
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
