using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartBank.Application.Features.Admin.Transactions.ApproveTransaction;
using SmartBank.Application.Features.Admin.Transactions.RejectTransaction;
using SmartBank.Application.Features.User.Transactions.AddTransaction;
using SmartBank.Application.Features.User.Transactions.GetTransactions;
using SmartBank.Shared.Dtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SmartBank.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            _httpContextAccessor = httpContextAccessor;
        }
       

        // POST api/<AccountController>
        [HttpPost]
        [Authorize(Roles ="User" , Policy ="UserOnly")]
        public async Task<AddTransactionDto> Post([FromBody] AddTransactionDto transaction)
        {

                var userId = _httpContextAccessor.HttpContext.User.Claims.Where(c=>c.Type =="uid").FirstOrDefault().Value;
                var command = new AddTransactionCommand(transaction,Guid.Parse(userId));
                var res = await mediator.Send(command);
                return res;

        }

        // POST api/<AccountController>
        [HttpGet("{accountId}")]
        [Authorize(Roles = "User", Policy = "UserOnly")]
        public async Task<List<GetTransactionDto>> GetTransactionsHistory(Guid accountId)
        {

            var command = new GetTransactionCommand(accountId);
            var res = await mediator.Send(command);
            return res;

        }

        [HttpGet("{transactionId}")]
        [Authorize(Roles = "Admin", Policy = "AdminOnly")]
        public async Task<GetTransactionDto> ApproveTransactions(Guid transactionId)
        {

            var command = new ApproveTransactionCommand(transactionId);
            var res = await mediator.Send(command);
            return res;

        }

        [HttpGet("{transactionId}")]
        [Authorize(Roles = "Admin", Policy = "AdminOnly")]
        public async Task<GetTransactionDto> RejectTransactions(Guid transactionId)
        {

            var command = new RejectTransactionCommand(transactionId);
            var res = await mediator.Send(command);
            return res;

        }



    }

}