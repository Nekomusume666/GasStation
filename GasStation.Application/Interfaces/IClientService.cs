using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> GetClientByIdAsync(int id);
        Task AddClientAsync(ClientDto clientDto);
        Task<ClientDto> GetClientByLoginAsync(string login);
        Task UpdateClientAsync(int id, ClientDto clientDto);
        Task DeleteClientAsync(int id);
        Task DeleteAllClientsAsync();
    }
}
