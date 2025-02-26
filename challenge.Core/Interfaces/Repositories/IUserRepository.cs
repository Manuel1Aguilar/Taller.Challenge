using challenge.Core.Models;

namespace challenge.Core.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(string username);
}