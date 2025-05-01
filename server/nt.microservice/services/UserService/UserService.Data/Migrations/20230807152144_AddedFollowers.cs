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
                    { 3L, 2L, 4L },
                    { 4L, 3L, 5L },
                    { 5L, 4L, 6L },
                    { 6L, 5L, 7L },
                    { 7L, 6L, 8L },
                    { 8L, 7L, 9L },
                    { 9L, 8L, 10L },
                    { 10L, 9L, 11L },
                    { 11L, 10L, 12L },
                    { 12L, 11L, 13L },
                    { 13L, 12L, 14L },
                    { 14L, 13L, 15L },
                    { 15L, 14L, 16L },
                    { 16L, 15L, 17L },
                    { 17L, 16L, 18L },
                    { 18L, 17L, 19L },
                    { 19L, 18L, 20L },
                    { 20L, 19L, 21L },
                    { 21L, 20L, 22L },
                    { 22L, 21L, 23L },
                    { 23L, 22L, 24L },
                    { 24L, 23L, 25L },
                    { 25L, 24L, 26L },
                    { 26L, 25L, 27L },
                    { 27L, 26L, 28L },
                    { 28L, 27L, 29L },
                    { 29L, 28L, 30L },
                    { 30L, 29L, 31L },
                    { 31L, 30L, 32L },
                    { 32L, 31L, 33L },
                    { 33L, 32L, 34L },
                    { 34L, 33L, 35L },
                    { 35L, 34L, 36L },
                    { 36L, 35L, 37L },
                    { 37L, 36L, 38L },
                    { 38L, 37L, 39L },
                    { 39L, 38L, 40L },
                    { 40L, 39L, 41L },
                    { 41L, 40L, 42L },
                    { 42L, 41L, 43L },
                    { 43L, 42L, 44L },
                    { 44L, 43L, 45L },
                    { 45L, 44L, 46L },
                    { 46L, 45L, 47L },
                    { 47L, 46L, 48L },
                    { 48L, 47L, 49L },
                    { 49L, 48L, 50L },
                    { 50L, 49L, 1L },
                    { 51L, 1L, 50L },
                    { 52L, 2L, 1L },
                    { 53L, 3L, 2L },
                    { 54L, 4L, 5L }
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
        }
    }
}
