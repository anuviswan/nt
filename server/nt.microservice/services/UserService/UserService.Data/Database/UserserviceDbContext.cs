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
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserMetaInformation>()
                    .HasMany(x => x.Followers)
                    .WithOne(x => x.Follower)
                    .OnDelete(DeleteBehavior.ClientNoAction);


        modelBuilder.Entity<UserMetaInformation>()
                    .HasMany(x => x.Following)
                    .WithOne(x => x.Followee)
                    .OnDelete(DeleteBehavior.ClientNoAction);



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
            Id = 4,
            UserName = "admin",
            Bio = "I am Admin",
            DisplayName = "Admin",
        };

        var jiaFollowers = new[]
        {
            new UserFollower
            {
                Id = 1,
                FolloweeId = jia.Id,
                FollowerId  = naina.Id,
            },
            new UserFollower
            {
                Id = 2,
                FolloweeId = jia.Id,
                FollowerId = sreena.Id,
            }
        };

        var nainaFollowers = new[]
        {
            new UserFollower
            {
                Id = 3,
                FolloweeId = naina.Id,
                FollowerId = jia.Id,
            },
            new UserFollower
            {
                Id = 4,
                FolloweeId = naina.Id,
                FollowerId = sreena.Id,
            }
        };

        //jia.Followers = jiaFollowers;
        //naina.Followers = nainaFollowers;

        modelBuilder.Entity<UserMetaInformation>()
                .HasData(new UserMetaInformation[] { jia, naina, sreena, admin });

        modelBuilder.Entity<UserFollower>()
                .HasData(jiaFollowers.Concat(nainaFollowers).ToArray<UserFollower>());

        



    }

    public DbSet<UserMetaInformation> UserMetaInformation { get; set; }
    public DbSet<UserFollower> UserFollower { get; set; }

}
