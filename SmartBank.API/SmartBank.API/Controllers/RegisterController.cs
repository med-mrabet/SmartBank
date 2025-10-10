using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Application.Identity;
using SmartBank.Shared.Dtos;

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService _userService;
        public RegisterController( IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUser user)
        {
            var command = await this._userService.RegisterCustomer(user);
            return  Ok(command);   
        }
    }
}
