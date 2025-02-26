using challenge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace challenge.Infrastructure;

public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

}
