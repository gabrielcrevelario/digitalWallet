using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.DTO.Response;
using DigitalWallet.Aplication.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace wallet_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // OAuth2 ou JWT
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<TransactionResponse>> Create([FromBody] TransferRequest request)
        {
            var result = await _transactionService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        /// <summary>
        /// Obtém uma transação específica
        /// </summary>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<TransactionResponse>> GetById(Guid id)
        {
            var result = await _transactionService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet("user")]
        [Authorize]
        public async Task<IActionResult> GetUserTransactions([FromQuery] TransactionFilterRequest request)
        {
            var transactions = await _transactionService.GetUserTransactionsAsync(request.userId, request.StartDate, request.EndDate);
            return Ok(transactions);
        }
    }
}
