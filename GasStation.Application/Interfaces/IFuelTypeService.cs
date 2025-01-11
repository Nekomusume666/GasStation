using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IFuelTypeService
    {
        Task<IEnumerable<FuelTypeDto>> GetAllFuelTypesAsync();
        Task<FuelTypeDto> GetFuelTypeByIdAsync(int id);
        Task AddFuelTypeAsync(FuelTypeDto fuelTypeDto);
        Task UpdateFuelTypeAsync(int id, FuelTypeDto fuelTypeDto);
        Task DeleteFuelTypeAsync(int id);
        Task DeleteAllFuelTypesAsync();
    }
}
