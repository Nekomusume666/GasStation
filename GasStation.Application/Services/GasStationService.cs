using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class GasStationService : IGasStationService
    {
        private readonly ApplicationDbContext _context;

        public GasStationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GasStationDto>> GetAllGasStationsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var gasStations = await _context.GasStations
                    .Select(gs => new GasStationDto
                    {
                        ID_GasStation = gs.ID_GasStation,
                        Name = gs.Name,
                        Address = gs.Address,
                        Coordinates = gs.Coordinates,
                        ID_Administrator = (int)gs.ID_Administrator
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return gasStations;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<GasStationDto> GetGasStationByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var gasStation = await _context.GasStations.FindAsync(id);
                if (gasStation == null) throw new KeyNotFoundException("Gas station not found");

                var gasStationDto = new GasStationDto
                {
                    ID_GasStation = gasStation.ID_GasStation,
                    Name = gasStation.Name,
                    Address = gasStation.Address,
                    Coordinates = gasStation.Coordinates,
                    ID_Administrator = (int)gasStation.ID_Administrator
                };

                await transaction.CommitAsync();
                return gasStationDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddGasStationAsync(GasStationDto gasStationDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var gasStation = new Domain.Models.GasStation
                {
                    Name = gasStationDto.Name,
                    Address = gasStationDto.Address,
                    Coordinates = gasStationDto.Coordinates,
                    ID_Administrator = gasStationDto.ID_Administrator
                };

                _context.GasStations.Add(gasStation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateGasStationAsync(int id, GasStationDto gasStationDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var gasStation = await _context.GasStations.FindAsync(id);
                if (gasStation == null) throw new KeyNotFoundException("Gas station not found");

                gasStation.Name = gasStationDto.Name;
                gasStation.Address = gasStationDto.Address;
                gasStation.Coordinates = gasStationDto.Coordinates;
                gasStation.ID_Administrator = gasStationDto.ID_Administrator;

                _context.GasStations.Update(gasStation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteGasStationAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var gasStation = await _context.GasStations.FindAsync(id);
                if (gasStation == null) throw new KeyNotFoundException("Gas station not found");

                _context.GasStations.Remove(gasStation);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllGasStationsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.GasStations.RemoveRange(_context.GasStations);
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
