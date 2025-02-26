using challenge.Core.Dtos;
using challenge.Core.Interfaces.Repositories;
using challenge.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace challenge.Services;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly ILogger<UserService> _logger = logger;

    public async Task<Result<UserDto>> GetUserAsync(string username)
    {
        _logger.LogInformation("Getting user by username: {username}", username);
        try
        {

            var userObject = await _userRepository.GetUserAsync(username);
            if (userObject is null)
            {
                return Result<UserDto>.Failure("User not found.");
            }
            var userDto = new UserDto()
            {
                Username = userObject.Username,
                Name = userObject.Name
            };
            return Result<UserDto>.Success(userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error getting user by username: {username}. Error: {ex}", username, ex.Message);
            return Result<UserDto>.Failure("Exception getting user");
        }
    }
}
