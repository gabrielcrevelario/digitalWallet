using DigitalWallet.Domain.service;
using Microsoft.AspNetCore.Mvc;
using DigitalWallet.Aplication.DTO.Request;
using DigitalWallet.Aplication.interfaces;
using Microsoft.AspNetCore.Authorization;
namespace wallet_api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserRequest request)
        {
            var user = await _userService.CreateUserAsync(request);
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
