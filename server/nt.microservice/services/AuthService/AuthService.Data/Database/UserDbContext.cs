using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Data.Database;
public class UserDbContext:DbContext
{
    public UserDbContext()
    {
    }

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server = localhost; Database = NtUserDb; User Id = sa; Password = Admin123;");
    }
    public DbSet<User> Users { get; set; }
}
