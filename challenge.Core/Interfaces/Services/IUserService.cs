using challenge.Core.Dtos;

namespace challenge.Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<UserDto>> GetUserAsync(string username);
}