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
        var users = new List<UserMetaInformation>
        {
            new UserMetaInformation { Id = 1, UserName = "jia.anu", Bio = "I am jia anu", DisplayName = "Jia Anu" },
            new UserMetaInformation { Id = 2, UserName = "naina.anu", Bio = "I am naina anu", DisplayName = "Naina Anu" },
            new UserMetaInformation { Id = 3, UserName = "priya.smith", Bio = "I am priya smith", DisplayName = "Priya Smith" },
            new UserMetaInformation { Id = 4, UserName = "rajan.kumar", Bio = "I am rajan kumar", DisplayName = "Rajan Kumar" },
            new UserMetaInformation { Id = 5, UserName = "surya.karthik", Bio = "I am surya karthik", DisplayName = "Surya Karthik" },
            new UserMetaInformation { Id = 6, UserName = "anjali.menon", Bio = "I am anjali menon", DisplayName = "Anjali Menon" },
            new UserMetaInformation { Id = 7, UserName = "manju.pillai", Bio = "I am manju pillai", DisplayName = "Manju Pillai" },
            new UserMetaInformation { Id = 8, UserName = "reema.johnson", Bio = "I am reema johnson", DisplayName = "Reema Johnson" },
            new UserMetaInformation { Id = 9, UserName = "sona.roy", Bio = "I am sona roy", DisplayName = "Sona Roy" },
            new UserMetaInformation { Id = 10, UserName = "ajay.sharma", Bio = "I am ajay sharma", DisplayName = "Ajay Sharma" },
            new UserMetaInformation { Id = 11, UserName = "mukesh.singh", Bio = "I am mukesh singh", DisplayName = "Mukesh Singh" },
            new UserMetaInformation { Id = 12, UserName = "ranjan.verma", Bio = "I am ranjan verma", DisplayName = "Ranjan Verma" },
            new UserMetaInformation { Id = 13, UserName = "tina.kapoor", Bio = "I am tina kapoor", DisplayName = "Tina Kapoor" },
            new UserMetaInformation { Id = 14, UserName = "jayan.murali", Bio = "I am jayan murali", DisplayName = "Jayan Murali" },
            new UserMetaInformation { Id = 15, UserName = "shiva.singh", Bio = "I am shiva singh", DisplayName = "Shiva Singh" },
            new UserMetaInformation { Id = 16, UserName = "pradeep.gupta", Bio = "I am pradeep gupta", DisplayName = "Pradeep Gupta" },
            new UserMetaInformation { Id = 17, UserName = "seema.khan", Bio = "I am seema khan", DisplayName = "Seema Khan" },
            new UserMetaInformation { Id = 18, UserName = "geetha.singh", Bio = "I am geetha singh", DisplayName = "Geetha Singh" },
            new UserMetaInformation { Id = 19, UserName = "krishna.raj", Bio = "I am krishna raj", DisplayName = "Krishna Raj" },
            new UserMetaInformation { Id = 20, UserName = "babu.malhotra", Bio = "I am babu malhotra", DisplayName = "Babu Malhotra" },
            new UserMetaInformation { Id = 21, UserName = "pavi.kumar", Bio = "I am pavi kumar", DisplayName = "Pavi Kumar" },
            new UserMetaInformation { Id = 22, UserName = "manu.jain", Bio = "I am manu jain", DisplayName = "Manu Jain" },
            new UserMetaInformation { Id = 23, UserName = "rajesh.agarwal", Bio = "I am rajesh agarwal", DisplayName = "Rajesh Agarwal" },
            new UserMetaInformation { Id = 24, UserName = "neha.sharma", Bio = "I am neha sharma", DisplayName = "Neha Sharma" },
            new UserMetaInformation { Id = 25, UserName = "prabha.sen", Bio = "I am prabha sen", DisplayName = "Prabha Sen" },
            new UserMetaInformation { Id = 26, UserName = "krish.kapoor", Bio = "I am krish kapoor", DisplayName = "Krish Kapoor" },
            new UserMetaInformation { Id = 27, UserName = "sasi.roy", Bio = "I am sasi roy", DisplayName = "Sasi Roy" },
            new UserMetaInformation { Id = 28, UserName = "george.fernandes", Bio = "I am george fernandes", DisplayName = "George Fernandes" },
            new UserMetaInformation { Id = 29, UserName = "shalini.singh", Bio = "I am shalini singh", DisplayName = "Shalini Singh" },
            new UserMetaInformation { Id = 30, UserName = "varsha.menon", Bio = "I am varsha menon", DisplayName = "Varsha Menon" },
            new UserMetaInformation { Id = 31, UserName = "lalitha.nair", Bio = "I am lalitha nair", DisplayName = "Lalitha Nair" },
            new UserMetaInformation { Id = 32, UserName = "sunil.singh", Bio = "I am sunil singh", DisplayName = "Sunil Singh" },
            new UserMetaInformation { Id = 33, UserName = "manoj.sharma", Bio = "I am manoj sharma", DisplayName = "Manoj Sharma" },
            new UserMetaInformation { Id = 34, UserName = "radhika.bansal", Bio = "I am radhika bansal", DisplayName = "Radhika Bansal" },
            new UserMetaInformation { Id = 35, UserName = "ravi.kapoor", Bio = "I am ravi kapoor", DisplayName = "Ravi Kapoor" },
            new UserMetaInformation { Id = 36, UserName = "vijay.gupta", Bio = "I am vijay gupta", DisplayName = "Vijay Gupta" },
            new UserMetaInformation { Id = 37, UserName = "meera.nair", Bio = "I am meera nair", DisplayName = "Meera Nair" },
            new UserMetaInformation { Id = 38, UserName = "sundar.kumar", Bio = "I am sundar kumar", DisplayName = "Sundar Kumar" },
            new UserMetaInformation { Id = 39, UserName = "ajith.murali", Bio = "I am ajith murali", DisplayName = "Ajith Murali" },
            new UserMetaInformation { Id = 40, UserName = "chitra.singh", Bio = "I am chitra singh", DisplayName = "Chitra Singh" },
            new UserMetaInformation { Id = 41, UserName = "sunitha.khan", Bio = "I am sunitha khan", DisplayName = "Sunitha Khan" },
            new UserMetaInformation { Id = 42, UserName = "kishore.verma", Bio = "I am kishore verma", DisplayName = "Kishore Verma" },
            new UserMetaInformation { Id = 43, UserName = "rani.pillai", Bio = "I am rani pillai", DisplayName = "Rani Pillai" },
            new UserMetaInformation { Id = 44, UserName = "dinesh.patel", Bio = "I am dinesh patel", DisplayName = "Dinesh Patel" },
            new UserMetaInformation { Id = 45, UserName = "ragini.singh", Bio = "I am ragini singh", DisplayName = "Ragini Singh" },
            new UserMetaInformation { Id = 46, UserName = "kamal.rajan", Bio = "I am kamal rajan", DisplayName = "Kamal Rajan" },
            new UserMetaInformation { Id = 47, UserName = "kamini.roy", Bio = "I am kamini roy", DisplayName = "Kamini Roy" },
            new UserMetaInformation { Id = 48, UserName = "vishal.gupta", Bio = "I am vishal gupta", DisplayName = "Vishal Gupta" },
            new UserMetaInformation { Id = 49, UserName = "anjali.agarwal", Bio = "I am anjali agarwal", DisplayName = "Anjali Agarwal" },
            new UserMetaInformation { Id = 50, UserName = "pravin.kumar", Bio = "I am pravin kumar", DisplayName = "Pravin Kumar" }
        };

        var userFollowers = new List<UserFollower>
        {
            new UserFollower { Id = 1, FollowerId = 1, FolloweeId = 2 },
            new UserFollower { Id = 2, FollowerId = 1, FolloweeId = 3 },
            new UserFollower { Id = 3, FollowerId = 2, FolloweeId = 4 },
            new UserFollower { Id = 4, FollowerId = 3, FolloweeId = 5 },
            new UserFollower { Id = 5, FollowerId = 4, FolloweeId = 6 },
            new UserFollower { Id = 6, FollowerId = 5, FolloweeId = 7 },
            new UserFollower { Id = 7, FollowerId = 6, FolloweeId = 8 },
            new UserFollower { Id = 8, FollowerId = 7, FolloweeId = 9 },
            new UserFollower { Id = 9, FollowerId = 8, FolloweeId = 10 },
            new UserFollower { Id = 10, FollowerId = 9, FolloweeId = 11 },
            new UserFollower { Id = 11, FollowerId = 10, FolloweeId = 12 },
            new UserFollower { Id = 12, FollowerId = 11, FolloweeId = 13 },
            new UserFollower { Id = 13, FollowerId = 12, FolloweeId = 14 },
            new UserFollower { Id = 14, FollowerId = 13, FolloweeId = 15 },
            new UserFollower { Id = 15, FollowerId = 14, FolloweeId = 16 },
            new UserFollower { Id = 16, FollowerId = 15, FolloweeId = 17 },
            new UserFollower { Id = 17, FollowerId = 16, FolloweeId = 18 },
            new UserFollower { Id = 18, FollowerId = 17, FolloweeId = 19 },
            new UserFollower { Id = 19, FollowerId = 18, FolloweeId = 20 },
            new UserFollower { Id = 20, FollowerId = 19, FolloweeId = 21 },
            new UserFollower { Id = 21, FollowerId = 20, FolloweeId = 22 },
            new UserFollower { Id = 22, FollowerId = 21, FolloweeId = 23 },
            new UserFollower { Id = 23, FollowerId = 22, FolloweeId = 24 },
            new UserFollower { Id = 24, FollowerId = 23, FolloweeId = 25 },
            new UserFollower { Id = 25, FollowerId = 24, FolloweeId = 26 },
            new UserFollower { Id = 26, FollowerId = 25, FolloweeId = 27 },
            new UserFollower { Id = 27, FollowerId = 26, FolloweeId = 28 },
            new UserFollower { Id = 28, FollowerId = 27, FolloweeId = 29 },
            new UserFollower { Id = 29, FollowerId = 28, FolloweeId = 30 },
            new UserFollower { Id = 30, FollowerId = 29, FolloweeId = 31 },
            new UserFollower { Id = 31, FollowerId = 30, FolloweeId = 32 },
            new UserFollower { Id = 32, FollowerId = 31, FolloweeId = 33 },
            new UserFollower { Id = 33, FollowerId = 32, FolloweeId = 34 },
            new UserFollower { Id = 34, FollowerId = 33, FolloweeId = 35 },
            new UserFollower { Id = 35, FollowerId = 34, FolloweeId = 36 },
            new UserFollower { Id = 36, FollowerId = 35, FolloweeId = 37 },
            new UserFollower { Id = 37, FollowerId = 36, FolloweeId = 38 },
            new UserFollower { Id = 38, FollowerId = 37, FolloweeId = 39 },
            new UserFollower { Id = 39, FollowerId = 38, FolloweeId = 40 },
            new UserFollower { Id = 40, FollowerId = 39, FolloweeId = 41 },
            new UserFollower { Id = 41, FollowerId = 40, FolloweeId = 42 },
            new UserFollower { Id = 42, FollowerId = 41, FolloweeId = 43 },
            new UserFollower { Id = 43, FollowerId = 42, FolloweeId = 44 },
            new UserFollower { Id = 44, FollowerId = 43, FolloweeId = 45 },
            new UserFollower { Id = 45, FollowerId = 44, FolloweeId = 46 },
            new UserFollower { Id = 46, FollowerId = 45, FolloweeId = 47 },
            new UserFollower { Id = 47, FollowerId = 46, FolloweeId = 48 },
            new UserFollower { Id = 48, FollowerId = 47, FolloweeId = 49 },
            new UserFollower { Id = 49, FollowerId = 48, FolloweeId = 50 },
            new UserFollower { Id = 50, FollowerId = 49, FolloweeId = 1 },
            new UserFollower { Id = 51, FollowerId = 1, FolloweeId = 50 },
            new UserFollower { Id = 52, FollowerId = 2, FolloweeId = 1 },
            new UserFollower { Id = 53, FollowerId = 3, FolloweeId = 2 },
            new UserFollower { Id = 54, FollowerId = 4, FolloweeId = 5 },

        };


        modelBuilder.Entity<UserMetaInformation>()
                .HasData(users);

        modelBuilder.Entity<UserFollower>()
                .HasData(userFollowers);

    }

    public DbSet<UserMetaInformation> UserMetaInformation { get; set; }
    public DbSet<UserFollower> UserFollower { get; set; }

}
