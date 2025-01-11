using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class PumpService : IPumpService
    {
        private readonly ApplicationDbContext _context;

        public PumpService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PumpDto>> GetAllPumpsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pumps = await _context.Pumps
                    .Select(p => new PumpDto
                    {
                        ID_Pump = p.ID_Pump,
                        ID_GasStation = p.ID_GasStation,
                        ID_FuelType = p.ID_FuelType
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return pumps;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<PumpDto> GetPumpByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pump = await _context.Pumps.FindAsync(id);
                if (pump == null) throw new KeyNotFoundException("Pump not found");

                var pumpDto = new PumpDto
                {
                    ID_Pump = pump.ID_Pump,
                    ID_GasStation = pump.ID_GasStation,
                    ID_FuelType = pump.ID_FuelType
                };

                await transaction.CommitAsync();
                return pumpDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddPumpAsync(PumpDto pumpDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pump = new Pump
                {
                    ID_GasStation = pumpDto.ID_GasStation,
                    ID_FuelType = pumpDto.ID_FuelType
                };

                _context.Pumps.Add(pump);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdatePumpAsync(int id, PumpDto pumpDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pump = await _context.Pumps.FindAsync(id);
                if (pump == null) throw new KeyNotFoundException("Pump not found");

                pump.ID_GasStation = pumpDto.ID_GasStation;
                pump.ID_FuelType = pumpDto.ID_FuelType;

                _context.Pumps.Update(pump);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeletePumpAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var pump = await _context.Pumps.FindAsync(id);
                if (pump == null) throw new KeyNotFoundException("Pump not found");

                _context.Pumps.Remove(pump);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllPumpsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Pumps.RemoveRange(_context.Pumps);
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
