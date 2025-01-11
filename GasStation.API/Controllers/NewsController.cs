using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var news = await _newsService.GetAllNewsAsync();

            if (news == null || !news.Any())
            {
                return NotFound("No news found.");
            }

            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);

            if (newsItem == null)
            {
                return NotFound($"News item with ID {id} not found.");
            }

            return Ok(newsItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsDto newsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _newsService.AddNewsAsync(newsDto);
                return CreatedAtAction(nameof(GetById), new { id = newsDto.ID_News }, newsDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the news item: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsDto newsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingNews = await _newsService.GetNewsByIdAsync(id);

            if (existingNews == null)
            {
                return NotFound($"News item with ID {id} not found.");
            }

            try
            {
                await _newsService.UpdateNewsAsync(id, newsDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the news item: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingNews = await _newsService.GetNewsByIdAsync(id);

            if (existingNews == null)
            {
                return NotFound($"News item with ID {id} not found.");
            }

            try
            {
                await _newsService.DeleteNewsAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the news item: {ex.Message}");
            }
        }
    }
}
