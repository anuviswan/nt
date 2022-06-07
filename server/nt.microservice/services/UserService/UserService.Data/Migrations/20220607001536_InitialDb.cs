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
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    { 3L, "I am sreena anu", "Sreena Anu", "sreena.anu" },
                    { 4L, "I am admin", "Admin", "admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMetaInformation");
        }
    }
}
