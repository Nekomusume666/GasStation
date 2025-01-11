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
            return Ok(news);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var newsItem = await _newsService.GetNewsByIdAsync(id);
            return Ok(newsItem);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NewsDto newsDto)
        {
            await _newsService.AddNewsAsync(newsDto);
            return CreatedAtAction(nameof(GetById), new { id = newsDto.ID_News }, newsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewsDto newsDto)
        {
            await _newsService.UpdateNewsAsync(id, newsDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsService.DeleteNewsAsync(id);
            return NoContent();
        }
    }
}
