using Microsoft.EntityFrameworkCore;

namespace UserService.Data.Database;
public class UserserviceDbContext : DbContext
{
    public UserserviceDbContext()
    {
    }

    public UserserviceDbContext(DbContextOptions<UserserviceDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Server = host.docker.internal; Database = NtUserDb; User Id = sa; Password = Admin123;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserMetaInformation>()
                    .HasData(
            new UserMetaInformation
            {
                Id = 1,
                UserName = "jia.anu",
                Bio = "I am Jia anu",
                DisplayName = "Jia Anu",
            },
            new UserMetaInformation
            {
                Id = 2,
                UserName = "naina.anu",
                Bio = "I am Naina anu",
                DisplayName = "Naina Anu",
            },
            new UserMetaInformation
            {
                Id = 3,
                UserName = "sreena.anu",
                Bio = "I am Sreena anu",
                DisplayName = "Sreena Anu",
            },
            new UserMetaInformation
            {
                Id = 4,
                UserName = "admin",
                Bio = "I am admin",
                DisplayName = "Admin",
            });
    }
    public DbSet<UserMetaInformation> UserMetaInformation { get; set; }

}
