using GasStation.Application.DTOs;
using GasStation.Application.Interfaces;
using GasStation.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GasStation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService _transactionService;

        public TransactionsController(ITransactionsService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();

            if (transactions == null || !transactions.Any())
            {
                return NotFound("No transactions found.");
            }

            return Ok(transactions);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);

            if (transaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            return Ok(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionsDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _transactionService.AddTransactionAsync(transactionDto);
                return CreatedAtAction(nameof(GetById), new { id = transactionDto.ID_Transactions }, transactionDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while creating the transaction: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionsDto transactionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);

            if (existingTransaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            try
            {
                await _transactionService.UpdateTransactionAsync(id, transactionDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the transaction: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTransaction = await _transactionService.GetTransactionByIdAsync(id);

            if (existingTransaction == null)
            {
                return NotFound($"Transaction with ID {id} not found.");
            }

            try
            {
                await _transactionService.DeleteTransactionAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the transaction: {ex.Message}");
            }
        }
    }
}
