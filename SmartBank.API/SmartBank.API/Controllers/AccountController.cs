using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Application.Features.Admin.Accounts.FreezAccount;
using SmartBank.Application.Features.User.Account.Commands.AddAccount;
using SmartBank.Application.Features.User.Account.Commands.FreezAccount;
using SmartBank.Application.Features.User.Account.Queries.GetAccount;
using SmartBank.Application.Features.User.Account.Queries.GetAccounts;
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
        [HttpGet("{accountId}")]
        [Authorize(Roles = "User", Policy = "UserOnly")]
        public async Task<GetAccountDto> GetAccount(Guid accountId)
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "uid").FirstOrDefault().Value;
            var query = new GetAccountQuery(Guid.Parse(userId),accountId);
            var res = await mediator.Send(query);
            return res;
        }

        // GET api/<AccountController>/5
        [HttpGet("")]
        [Authorize(Roles = "User", Policy = "UserOnly")]
        public async  Task<IList<GetAccountDto>> GetAccountsByUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "uid").FirstOrDefault().Value;
            var query = new GetAccountsQuery(Guid.Parse(userId));
            var res =  await mediator.Send(query);
            return res;
        }

        // POST api/<AccountController>
        [HttpPost]
        [Authorize(Roles ="User" , Policy ="UserOnly")]
        public async Task<bool> Post([FromBody] AccountDto account)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c=>c.Type =="uid").FirstOrDefault().Value;
                var command = new AddAccountCommand(account,Guid.Parse(userId));
                var res = await mediator.Send(command);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        [HttpPost("{accountId}")]
        [Authorize(Roles = "User", Policy = "UserOnly")]
        public async Task<bool> FreezAccount(Guid accountId)
        {
            try
            {
                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "uid").FirstOrDefault().Value;
                var command = new FreezAccountCommand(Guid.Parse(userId),accountId);
                var res = await mediator.Send(command);
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        [HttpPut("{accountId}")]
        [Authorize(Roles = "Admin", Policy = "AdminOnly")]
        public async Task<AccountDto> FreezAccountByAdmin(Guid accountId)
        {
                var command = new FreezAccountByAdminCommand(accountId );
                var res = await mediator.Send(command);
                return res;

        }

    }
}
