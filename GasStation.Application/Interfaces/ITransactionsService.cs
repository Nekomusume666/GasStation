using GasStation.Application.DTOs;

namespace GasStation.Application.Interfaces
{
    public interface ITransactionsService
    {
        Task<IEnumerable<TransactionsDto>> GetAllTransactionsAsync();
        Task<TransactionsDto> GetTransactionByIdAsync(int id);
        Task AddTransactionAsync(TransactionsDto transactionsDto);
        Task UpdateTransactionAsync(int id, TransactionsDto transactionsDto);
        Task DeleteTransactionAsync(int id);
        Task DeleteAllTransactionsAsync();
    }
}
