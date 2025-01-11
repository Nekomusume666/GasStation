using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface IGasStationService
    {
        Task<IEnumerable<GasStationDto>> GetAllGasStationsAsync();
        Task<GasStationDto> GetGasStationByIdAsync(int id);
        Task AddGasStationAsync(GasStationDto gasStationDto);
        Task UpdateGasStationAsync(int id, GasStationDto gasStationDto);
        Task DeleteGasStationAsync(int id);
        Task DeleteAllGasStationsAsync();
    }
}
