using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Data.Database;
public class UserManagementDbContext : DbContext
{
    public UserManagementDbContext()
    {
    }

    public UserManagementDbContext(DbContextOptions<UserManagementDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server = localhost; Database = NtUserDb; User Id = sa; Password = Admin123;");
    }
    public DbSet<User> Users { get; set; }
    public DbSet<UserMetaInformation> UsersInfo { get; set; }
}
