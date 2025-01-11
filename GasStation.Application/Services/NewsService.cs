using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Domain.Models;
using GasStation.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace GasStation.Application.Services
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;

        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsDto>> GetAllNewsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var newsList = await _context.News
                    .Select(n => new NewsDto
                    {
                        ID_News = n.ID_News,
                        Title = n.Title,
                        Description = n.Description,
                        StartDate = n.StartDate,
                        EndDate = n.EndDate,
                        ID_Administrator = n.ID_Administrator
                    })
                    .ToListAsync();

                await transaction.CommitAsync();
                return newsList;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<NewsDto> GetNewsByIdAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null) throw new KeyNotFoundException("News not found");

                var newsDto = new NewsDto
                {
                    ID_News = news.ID_News,
                    Title = news.Title,
                    Description = news.Description,
                    StartDate = news.StartDate,
                    EndDate = news.EndDate,
                    ID_Administrator = news.ID_Administrator
                };

                await transaction.CommitAsync();
                return newsDto;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task AddNewsAsync(NewsDto newsDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var news = new News
                {
                    Title = newsDto.Title,
                    Description = newsDto.Description,
                    StartDate = newsDto.StartDate,
                    EndDate = newsDto.EndDate,
                    ID_Administrator = newsDto.ID_Administrator
                };

                _context.News.Add(news);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task UpdateNewsAsync(int id, NewsDto newsDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null) throw new KeyNotFoundException("News not found");

                news.Title = newsDto.Title;
                news.Description = newsDto.Description;
                news.StartDate = newsDto.StartDate;
                news.EndDate = newsDto.EndDate;
                news.ID_Administrator = newsDto.ID_Administrator;

                _context.News.Update(news);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteNewsAsync(int id)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var news = await _context.News.FindAsync(id);
                if (news == null) throw new KeyNotFoundException("News not found");

                _context.News.Remove(news);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task DeleteAllNewsAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.News.RemoveRange(_context.News);
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
