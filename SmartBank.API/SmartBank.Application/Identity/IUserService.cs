using SmartBank.Shared.Dtos;
using SmartBank.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartBank.Application.Identity
{
    public interface IUserService
    {

        Task<Boolean> RegisterCustomer(RegisterUser user);
        Task<ApplicationUser?> ExistByIdAsync(Guid userId);

    }
}
