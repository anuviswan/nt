using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Data.Migrations
{
    public partial class InitialDb : Migration
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
                    { 42L, "I am kishore verma", "Kishore Verma", "kishore.verma" },
                    { 43L, "I am rani pillai", "Rani Pillai", "rani.pillai" },
                    { 44L, "I am dinesh patel", "Dinesh Patel", "dinesh.patel" },
                    { 45L, "I am ragini singh", "Ragini Singh", "ragini.singh" },
                    { 46L, "I am kamal rajan", "Kamal Rajan", "kamal.rajan" },
                    { 47L, "I am kamini roy", "Kamini Roy", "kamini.roy" },
                    { 48L, "I am vishal gupta", "Vishal Gupta", "vishal.gupta" },
                    { 49L, "I am anjali agarwal", "Anjali Agarwal", "anjali.agarwal" },
                    { 50L, "I am pravin kumar", "Pravin Kumar", "pravin.kumar" }
                });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMetaInformation");
        }
    }
}
