using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Application.Identity;
using SmartBank.Shared.Dtos;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }



        [HttpPost]

        public async Task<string> Login(LoginDto loginDto)
        {
            try
            {
                var token = await _authService.LoginAsync(loginDto);
                return token.Token;
            }
            catch (Exception ex) { throw; }
        }
    }
}
