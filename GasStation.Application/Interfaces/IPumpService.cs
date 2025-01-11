using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IPumpService
    {
        Task<IEnumerable<PumpDto>> GetAllPumpsAsync();
        Task<PumpDto> GetPumpByIdAsync(int id);
        Task AddPumpAsync(PumpDto pumpDto);
        Task UpdatePumpAsync(int id, PumpDto pumpDto);
        Task DeletePumpAsync(int id);
        Task DeleteAllPumpsAsync();
    }
}
