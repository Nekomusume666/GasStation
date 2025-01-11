using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IFuelService
    {
        Task<IEnumerable<FuelDto>> GetAllFuelsAsync();
        Task<FuelDto> GetFuelByIdAsync(int id);
        Task AddFuelAsync(FuelDto fuelDto);
        Task UpdateFuelAsync(int id, FuelDto fuelDto);
        Task DeleteFuelAsync(int id);
        Task DeleteAllFuelsAsync();
    }
}
