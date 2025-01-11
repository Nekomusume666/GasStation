using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface ISupplyService
    {
        Task<IEnumerable<SupplyDto>> GetAllSuppliesAsync();
        Task<SupplyDto> GetSupplyByIdAsync(int id);
        Task AddSupplyAsync(SupplyDto supplyDto);
        Task UpdateSupplyAsync(int id, SupplyDto supplyDto);
        Task DeleteSupplyAsync(int id);
        Task DeleteAllSuppliesAsync();
    }
}
