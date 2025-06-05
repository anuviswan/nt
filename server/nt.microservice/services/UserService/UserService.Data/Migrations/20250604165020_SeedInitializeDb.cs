using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Data.Migrations
{
    public partial class SeedInitializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMetaInformation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMetaInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFollower",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowerId = table.Column<long>(type: "bigint", nullable: false),
                    FolloweeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollower", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFollower_UserMetaInformation_FolloweeId",
                        column: x => x.FolloweeId,
                        principalTable: "UserMetaInformation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserFollower_UserMetaInformation_FollowerId",
                        column: x => x.FollowerId,
                        principalTable: "UserMetaInformation",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "UserMetaInformation",
                columns: new[] { "Id", "Bio", "DisplayName", "UserName" },
                values: new object[,]
                {
                    { 1L, "I am jia anu", "Jia Anu", "jia.anu" },
                    { 2L, "I am naina anu", "Naina Anu", "naina.anu" },
                    { 3L, "I am priya smith", "Priya Smith", "priya.smith" },
                    { 4L, "I am rajan kumar", "Rajan Kumar", "rajan.kumar" },
                    { 5L, "I am surya karthik", "Surya Karthik", "surya.karthik" },
                    { 6L, "I am anjali menon", "Anjali Menon", "anjali.menon" },
                    { 7L, "I am manju pillai", "Manju Pillai", "manju.pillai" },
                    { 8L, "I am reema johnson", "Reema Johnson", "reema.johnson" },
                    { 9L, "I am sona roy", "Sona Roy", "sona.roy" },
                    { 10L, "I am ajay sharma", "Ajay Sharma", "ajay.sharma" },
                    { 11L, "I am mukesh singh", "Mukesh Singh", "mukesh.singh" },
                    { 12L, "I am ranjan verma", "Ranjan Verma", "ranjan.verma" },
                    { 13L, "I am tina kapoor", "Tina Kapoor", "tina.kapoor" },
                    { 14L, "I am jayan murali", "Jayan Murali", "jayan.murali" },
                    { 15L, "I am shiva singh", "Shiva Singh", "shiva.singh" },
                    { 16L, "I am pradeep gupta", "Pradeep Gupta", "pradeep.gupta" },
                    { 17L, "I am seema khan", "Seema Khan", "seema.khan" },
                    { 18L, "I am geetha singh", "Geetha Singh", "geetha.singh" },
                    { 19L, "I am krishna raj", "Krishna Raj", "krishna.raj" },
                    { 20L, "I am babu malhotra", "Babu Malhotra", "babu.malhotra" },
                    { 21L, "I am pavi kumar", "Pavi Kumar", "pavi.kumar" },
                    { 22L, "I am manu jain", "Manu Jain", "manu.jain" },
                    { 23L, "I am rajesh agarwal", "Rajesh Agarwal", "rajesh.agarwal" },
                    { 24L, "I am neha sharma", "Neha Sharma", "neha.sharma" },
                    { 25L, "I am prabha sen", "Prabha Sen", "prabha.sen" },
                    { 26L, "I am krish kapoor", "Krish Kapoor", "krish.kapoor" },
                    { 27L, "I am sasi roy", "Sasi Roy", "sasi.roy" },
                    { 28L, "I am george fernandes", "George Fernandes", "george.fernandes" },
                    { 29L, "I am shalini singh", "Shalini Singh", "shalini.singh" },
                    { 30L, "I am varsha menon", "Varsha Menon", "varsha.menon" },
                    { 31L, "I am lalitha nair", "Lalitha Nair", "lalitha.nair" },
                    { 32L, "I am sunil singh", "Sunil Singh", "sunil.singh" },
                    { 33L, "I am manoj sharma", "Manoj Sharma", "manoj.sharma" },
                    { 34L, "I am radhika bansal", "Radhika Bansal", "radhika.bansal" },
                    { 35L, "I am ravi kapoor", "Ravi Kapoor", "ravi.kapoor" },
                    { 36L, "I am vijay gupta", "Vijay Gupta", "vijay.gupta" },
                    { 37L, "I am meera nair", "Meera Nair", "meera.nair" },
                    { 38L, "I am sundar kumar", "Sundar Kumar", "sundar.kumar" },
                    { 39L, "I am ajith murali", "Ajith Murali", "ajith.murali" },
                    { 40L, "I am chitra singh", "Chitra Singh", "chitra.singh" },
                    { 41L, "I am sunitha khan", "Sunitha Khan", "sunitha.khan" },
                    { 42L, "I am kishore verma", "Kishore Verma", "kishore.verma" }
                });

            migrationBuilder.InsertData(
                table: "UserMetaInformation",
                columns: new[] { "Id", "Bio", "DisplayName", "UserName" },
                values: new object[,]
                {
                    { 43L, "I am rani pillai", "Rani Pillai", "rani.pillai" },
                    { 44L, "I am dinesh patel", "Dinesh Patel", "dinesh.patel" },
                    { 45L, "I am ragini singh", "Ragini Singh", "ragini.singh" },
                    { 46L, "I am kamal rajan", "Kamal Rajan", "kamal.rajan" },
                    { 47L, "I am kamini roy", "Kamini Roy", "kamini.roy" },
                    { 48L, "I am vishal gupta", "Vishal Gupta", "vishal.gupta" },
                    { 49L, "I am anjali agarwal", "Anjali Agarwal", "anjali.agarwal" },
                    { 50L, "I am pravin kumar", "Pravin Kumar", "pravin.kumar" }
                });

            migrationBuilder.InsertData(
                table: "UserFollower",
                columns: new[] { "Id", "FolloweeId", "FollowerId" },
                values: new object[,]
                {
                    { 1L, 2L, 1L },
                    { 2L, 3L, 1L },
                    { 3L, 4L, 2L },
                    { 4L, 5L, 3L },
                    { 5L, 6L, 4L },
                    { 6L, 7L, 5L },
                    { 7L, 8L, 6L },
                    { 8L, 9L, 7L },
                    { 9L, 10L, 8L },
                    { 10L, 11L, 9L },
                    { 11L, 12L, 10L },
                    { 12L, 13L, 11L },
                    { 13L, 14L, 12L },
                    { 14L, 15L, 13L },
                    { 15L, 16L, 14L },
                    { 16L, 17L, 15L },
                    { 17L, 18L, 16L },
                    { 18L, 19L, 17L },
                    { 19L, 20L, 18L },
                    { 20L, 21L, 19L },
                    { 21L, 22L, 20L },
                    { 22L, 23L, 21L },
                    { 23L, 24L, 22L },
                    { 24L, 25L, 23L },
                    { 25L, 26L, 24L },
                    { 26L, 27L, 25L },
                    { 27L, 28L, 26L },
                    { 28L, 29L, 27L },
                    { 29L, 30L, 28L },
                    { 30L, 31L, 29L },
                    { 31L, 32L, 30L },
                    { 32L, 33L, 31L },
                    { 33L, 34L, 32L },
                    { 34L, 35L, 33L },
                    { 35L, 36L, 34L },
                    { 36L, 37L, 35L },
                    { 37L, 38L, 36L },
                    { 38L, 39L, 37L },
                    { 39L, 40L, 38L },
                    { 40L, 41L, 39L },
                    { 41L, 42L, 40L },
                    { 42L, 43L, 41L }
                });

            migrationBuilder.InsertData(
                table: "UserFollower",
                columns: new[] { "Id", "FolloweeId", "FollowerId" },
                values: new object[,]
                {
                    { 43L, 44L, 42L },
                    { 44L, 45L, 43L },
                    { 45L, 46L, 44L },
                    { 46L, 47L, 45L },
                    { 47L, 48L, 46L },
                    { 48L, 49L, 47L },
                    { 49L, 50L, 48L },
                    { 50L, 1L, 49L },
                    { 51L, 50L, 1L },
                    { 52L, 1L, 2L },
                    { 53L, 2L, 3L },
                    { 54L, 5L, 4L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FolloweeId",
                table: "UserFollower",
                column: "FolloweeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollower_FollowerId",
                table: "UserFollower",
                column: "FollowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFollower");

            migrationBuilder.DropTable(
                name: "UserMetaInformation");
        }
    }
}
