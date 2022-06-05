using Microsoft.EntityFrameworkCore;

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
    public DbSet<UserMetaInformation> UserMetaInformation { get; set; }
}
