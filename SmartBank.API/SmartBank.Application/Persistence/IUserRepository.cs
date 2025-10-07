using SmartBank.Shared.Dtos;

namespace SmartBank.Application.Persistence;

public interface IUserRepository
{
    Task<List<UserDto>> GetCustomer();
    Task<UserDto> GetCustomer(string userId);
    Task<Boolean> RegisterCustomer(RegisterUser user);
}