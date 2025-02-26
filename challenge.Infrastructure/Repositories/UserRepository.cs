using challenge.Core.Interfaces.Repositories;
using challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace challenge.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;
    private readonly ILogger<UserRepository> _logger;
    public UserRepository(UserContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<User?> GetUserAsync(string username)
    {
        _logger.LogInformation("Getting user by username: {username}", username);
        return await _context.Users.SingleOrDefaultAsync(u => u.Username != null && u.Username.Equals(username));
    }

}
