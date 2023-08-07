using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Data.Migrations
{
    public partial class AddedFollowers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserMetaInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "UserMetaInformation",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                table: "UserFollower",
                columns: new[] { "Id", "FolloweeId", "FollowerId" },
                values: new object[,]
                {
                    { 1L, 1L, 2L },
                    { 2L, 1L, 3L },
                    { 3L, 2L, 1L },
                    { 4L, 2L, 3L }
                });

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Bio",
                value: "I am Jia anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Bio",
                value: "I am Naina anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Bio",
                value: "I am Sreena anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Bio",
                value: "I am Admin");

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

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "UserMetaInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Bio",
                table: "UserMetaInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Bio",
                value: "I am jia anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Bio",
                value: "I am naina anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Bio",
                value: "I am sreena anu");

            migrationBuilder.UpdateData(
                table: "UserMetaInformation",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Bio",
                value: "I am admin");
        }
    }
}
