using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly ApplicationDbContext _context;

        public TransactionsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransactionsDto>> GetAllTransactionsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transactions = await _context.Transactions
                    .Select(t => new TransactionsDto
                    {
                        ID_Transactions = t.ID_Transactions,
                        ID_Client = t.ID_Client,
                        ID_Fuel = t.ID_Fuel,
                        Quantity = t.Quantity,
                        Cost = t.Cost,
                        Date = t.Date,
                        BonusPoints = t.BonusPoints,
                        ID_Pump = t.ID_Pump
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return transactions;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<TransactionsDto>> GetTransactionsByClientIdAsync(int clientId)
        {
            try
            {
                var transactions = await _context.Transactions
                    .Where(t => t.ID_Client == clientId)
                    .Select(t => new TransactionsDto
                    {
                        ID_Transactions = t.ID_Transactions,
                        ID_Client = t.ID_Client,
                        ID_Fuel = t.ID_Fuel,
                        Quantity = t.Quantity,
                        Cost = t.Cost,
                        Date = t.Date,
                        BonusPoints = t.BonusPoints,
                        ID_Pump = t.ID_Pump
                    })
                    .ToListAsync();

                return transactions;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching transactions for client with ID {clientId}: {ex.Message}");
            }
        }


        public async Task<TransactionsDto> GetTransactionByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transactionEntity = await _context.Transactions.FindAsync(id);
                if (transactionEntity == null) throw new KeyNotFoundException("Transaction not found");

                var transactionDto = new TransactionsDto
                {
                    ID_Transactions = transactionEntity.ID_Transactions,
                    ID_Client = transactionEntity.ID_Client,
                    ID_Fuel = transactionEntity.ID_Fuel,
                    Quantity = transactionEntity.Quantity,
                    Cost = transactionEntity.Cost,
                    Date = transactionEntity.Date,
                    BonusPoints = transactionEntity.BonusPoints,
                    ID_Pump = transactionEntity.ID_Pump
                };

                await transaction.CommitAsync();
                return transactionDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddTransactionAsync(TransactionsDto transactionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transactionEntity = new Transactions
                {
                    ID_Client = transactionDto.ID_Client,
                    ID_Fuel = transactionDto.ID_Fuel,
                    Quantity = transactionDto.Quantity,
                    Cost = transactionDto.Cost,
                    Date = transactionDto.Date,
                    BonusPoints = transactionDto.BonusPoints,
                    ID_Pump = transactionDto.ID_Pump
                };

                _context.Transactions.Add(transactionEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateTransactionAsync(int id, TransactionsDto transactionDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transactionEntity = await _context.Transactions.FindAsync(id);
                if (transactionEntity == null) throw new KeyNotFoundException("Transaction not found");

                transactionEntity.ID_Client = transactionDto.ID_Client;
                transactionEntity.ID_Fuel = transactionDto.ID_Fuel;
                transactionEntity.Quantity = transactionDto.Quantity;
                transactionEntity.Cost = transactionDto.Cost;
                transactionEntity.Date = transactionDto.Date;
                transactionEntity.BonusPoints = transactionDto.BonusPoints;
                transactionEntity.ID_Pump = transactionDto.ID_Pump;

                _context.Transactions.Update(transactionEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteTransactionAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var transactionEntity = await _context.Transactions.FindAsync(id);
                if (transactionEntity == null) throw new KeyNotFoundException("Transaction not found");

                _context.Transactions.Remove(transactionEntity);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllTransactionsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Transactions.RemoveRange(_context.Transactions);
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
