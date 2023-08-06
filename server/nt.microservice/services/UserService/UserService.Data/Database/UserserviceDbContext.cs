using Microsoft.EntityFrameworkCore;

namespace UserService.Data.Database;
public class UserserviceDbContext : DbContext
{
    public UserserviceDbContext()
    {
    }

    public UserserviceDbContext(DbContextOptions<UserserviceDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
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
                    .HasMany(x => x.Followers)
                    .WithOne(x => x.Follower);

        modelBuilder.Entity<UserMetaInformation>()
                    .HasMany(x => x.Following)
                    .WithOne(x => x.Followee);


        SeedUsers(modelBuilder);

            //        .HasData(
            //new UserMetaInformation
            //{
            //    Id = 1,
            //    UserName = "jia.anu",
            //    Bio = "I am Jia anu",
            //    DisplayName = "Jia Anu",
            //},
            //new UserMetaInformation
            //{
            //    Id = 2,
            //    UserName = "naina.anu",
            //    Bio = "I am Naina anu",
            //    DisplayName = "Naina Anu",
            //},
            //new UserMetaInformation
            //{
            //    Id = 3,
            //    UserName = "sreena.anu",
            //    Bio = "I am Sreena anu",
            //    DisplayName = "Sreena Anu",
            //},
            //new UserMetaInformation
            //{
            //    Id = 4,
            //    UserName = "admin",
            //    Bio = "I am admin",
            //    DisplayName = "Admin",
            //});
    }

    private void SeedUsers(ModelBuilder modelBuilder)
    {
        var jia = new UserMetaInformation
        {
            Id = 1,
            UserName = "jia.anu",
            Bio = "I am Jia anu",
            DisplayName = "Jia Anu",
        };

        var naina = new UserMetaInformation
        {
            Id = 2,
            UserName = "naina.anu",
            Bio = "I am Naina anu",
            DisplayName = "Naina Anu",
        };

        var sreena = new UserMetaInformation
        {
            Id = 3,
            UserName = "sreena.anu",
            Bio = "I am Sreena anu",
            DisplayName = "Sreena Anu",
        };

        var admin = new UserMetaInformation
        {
            Id = 3,
            UserName = "admin",
            Bio = "I am Admin",
            DisplayName = "Admin",
        };

        var jiaFollowers = new[]
        {
            new UserFollower
            {
                Id = 1,
                Followee = jia,
                Follower = naina,
            },
            new UserFollower
            {
                Id = 2,
                Followee = jia,
                Follower = sreena,
            }
        };

        var nainaFollowers = new[]
        {
            new UserFollower
            {
                Id = 3,
                Followee = naina,
                Follower = jia,
            },
            new UserFollower
            {
                Id = 4,
                Followee = naina,
                Follower = sreena,
            }
        };

        jia.Followers = jiaFollowers;
        naina.Followers = nainaFollowers;

        modelBuilder.Entity<UserMetaInformation>()
                .HasData(jia, naina, sreena, admin);

        modelBuilder.Entity<UserFollower>()
            .HasData(jiaFollowers, nainaFollowers);
    }

    public DbSet<UserMetaInformation> UserMetaInformation { get; set; }
    public DbSet<UserFollower> UserFollower { get; set; }

}
