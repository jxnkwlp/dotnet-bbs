using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBBS.Web.Data.Migrations
{
    public partial class add_user_info_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    SiteUrl = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    WeiboId = table.Column<string>(nullable: true),
                    GitHubId = table.Column<string>(nullable: true),
                    UserSign = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UserInfo_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
