using challenge.Core.Interfaces.Repositories;
using challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace challenge.Infrastructure.Repositories;

public class UserRepository(UserContext context, ILogger<UserRepository> logger) : IUserRepository
{
    private readonly UserContext _context = context;
    private readonly ILogger<UserRepository> _logger = logger;

    public async Task<User?> GetUserAsync(string username)
    {
        _logger.LogInformation("Getting user by username: {username}", username);
        return await _context.Users.SingleOrDefaultAsync(u => u.Username != null && u.Username.Equals(username));
    }

}
