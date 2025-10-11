using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Application.Features.User.Account.Commands.AddAccount;
using SmartBank.Shared.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        [Authorize(Roles ="User" , Policy ="UserOnly")]
        public async Task<bool> Post([FromBody] AccountDto account)
        {
            try
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims.Where(c=>c.Type =="uid").FirstOrDefault().Value;
                var command = new AddAccountCommand(account);
                var res = await mediator.Send(command);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
