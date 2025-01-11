using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Получить всех клиентов
        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var clients = await _context.Clients
                    .Select(c => new ClientDto
                    {
                        ID_Client = c.ID_Client,
                        LastName = c.LastName,
                        FirstName = c.FirstName,
                        MiddleName = c.MiddleName,
                        Phone = c.Phone,
                        Email = c.Email,
                        Login = c.Login,
                        Password = c.Password,
                        BonusPoints = c.BonusPoints
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return clients;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Получить клиента по ID
        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) throw new KeyNotFoundException("Client not found");

                var clientDto = new ClientDto
                {
                    ID_Client = client.ID_Client,
                    LastName = client.LastName,
                    FirstName = client.FirstName,
                    MiddleName = client.MiddleName,
                    Phone = client.Phone,
                    Email = client.Email,
                    Login = client.Login,
                    Password = client.Password,
                    BonusPoints = client.BonusPoints
                };

                await transaction.CommitAsync();
                return clientDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Добавить нового клиента
        public async Task AddClientAsync(ClientDto clientDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var client = new Client
                {
                    LastName = clientDto.LastName,
                    FirstName = clientDto.FirstName,
                    MiddleName = clientDto.MiddleName,
                    Phone = clientDto.Phone,
                    Email = clientDto.Email,
                    Login = clientDto.Login,
                    Password = clientDto.Password,
                    BonusPoints = clientDto.BonusPoints
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Обновить существующего клиента
        public async Task UpdateClientAsync(int id, ClientDto clientDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) throw new KeyNotFoundException("Client not found");

                client.LastName = clientDto.LastName;
                client.FirstName = clientDto.FirstName;
                client.MiddleName = clientDto.MiddleName;
                client.Phone = clientDto.Phone;
                client.Email = clientDto.Email;
                client.Login = clientDto.Login;
                client.Password = clientDto.Password; // Обновлять с хэшированием
                client.BonusPoints = clientDto.BonusPoints;

                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Удалить клиента
        public async Task DeleteClientAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) throw new KeyNotFoundException("Client not found");

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Удалить всех клиентов
        public async Task DeleteAllClientsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.Clients.RemoveRange(_context.Clients);
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
