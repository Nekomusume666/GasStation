using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface INewsService
    {
        Task<IEnumerable<NewsDto>> GetAllNewsAsync();
        Task<NewsDto> GetNewsByIdAsync(int id);
        Task AddNewsAsync(NewsDto newsDto);
        Task UpdateNewsAsync(int id, NewsDto newsDto);
        Task DeleteNewsAsync(int id);
        Task DeleteAllNewsAsync();
    }
}
