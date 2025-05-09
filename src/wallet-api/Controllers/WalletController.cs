using DigitalWallet.Aplication.interfaces;
using DigitalWallet.Domain.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace wallet_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;

        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetWalletByUserId(Guid userId)
        {
            var wallet = await _walletService.GetWalletByUserIdAsync(userId);
            if (wallet == null) return NotFound();
            return Ok(wallet);
        }

        [HttpPost("{walletId}/deposit")]
        public async Task<IActionResult> Deposit(Guid walletId, [FromBody] decimal amount)
        {
            await _walletService.DepositAsync(walletId, amount);
            return Ok();
        }

        [HttpPost("{walletId}/withdraw")]
        public async Task<IActionResult> Withdraw(Guid walletId, [FromBody] decimal amount)
        {
            await _walletService.WithdrawAsync(walletId, amount);
            return Ok();
        }
    }
}
