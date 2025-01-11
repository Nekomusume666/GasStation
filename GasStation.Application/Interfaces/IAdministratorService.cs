using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IAdministratorService
    {
        Task<IEnumerable<AdministratorDto>> GetAllAdministratorsAsync();
        Task<AdministratorDto> GetAdministratorByIdAsync(int id);
        Task AddAdministratorAsync(AdministratorDto administratorDto);
        Task UpdateAdministratorAsync(int id, AdministratorDto administratorDto);
        Task DeleteAdministratorAsync(int id);
        Task DeleteAllAdministratorsAsync();
    }
}
