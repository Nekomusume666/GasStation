using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class SupplyService : ISupplyService
    {
        private readonly ApplicationDbContext _context;

        public SupplyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supplies = await _context.Supplies
                    .Select(s => new SupplyDto
                    {
                        ID_Supply = s.ID_Supply,
                        ID_GasStation = s.ID_GasStation,
                        SupplyDate = s.SupplyDate,
                        Quantity = (int)s.Quantity,
                        Cost = s.Cost,
                        ID_Employee = s.ID_Employee,
                        ID_Fuel = s.ID_Fuel
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return supplies;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<SupplyDto> GetSupplyByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supply = await _context.Supplies.FindAsync(id);
                if (supply == null) throw new KeyNotFoundException("Supply not found");

                var supplyDto = new SupplyDto
                {
                    ID_Supply = supply.ID_Supply,
                    ID_GasStation = supply.ID_GasStation,
                    SupplyDate = supply.SupplyDate,
                    Quantity = (int)supply.Quantity,
                    Cost = supply.Cost,
                    ID_Employee = supply.ID_Employee,
                    ID_Fuel = supply.ID_Fuel
                };

                await transaction.CommitAsync();
                return supplyDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddSupplyAsync(SupplyDto supplyDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supply = new Supply
                {
                    ID_GasStation = supplyDto.ID_GasStation,
                    SupplyDate = supplyDto.SupplyDate,
                    Quantity = supplyDto.Quantity,
                    Cost = supplyDto.Cost,
                    ID_Employee = supplyDto.ID_Employee,
                    ID_Fuel = supplyDto.ID_Fuel
                };

                _context.Supplies.Add(supply);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateSupplyAsync(int id, SupplyDto supplyDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supply = await _context.Supplies.FindAsync(id);
                if (supply == null) throw new KeyNotFoundException("Supply not found");

                supply.ID_GasStation = supplyDto.ID_GasStation;
                supply.SupplyDate = supplyDto.SupplyDate;
                supply.Quantity = supplyDto.Quantity;
                supply.Cost = supplyDto.Cost;
                supply.ID_Employee = supplyDto.ID_Employee;
                supply.ID_Fuel = supplyDto.ID_Fuel;

                _context.Supplies.Update(supply);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteSupplyAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var supply = await _context.Supplies.FindAsync(id);
                if (supply == null) throw new KeyNotFoundException("Supply not found");

                _context.Supplies.Remove(supply);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllSuppliesAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Supplies.RemoveRange(_context.Supplies);
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
