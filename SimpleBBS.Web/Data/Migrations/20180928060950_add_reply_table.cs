using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBBS.Web.Data.Migrations
{
    public partial class add_reply_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    TopicId = table.Column<long>(nullable: false),
                    ParentId = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    UpCount = table.Column<long>(nullable: false),
                    Forbided = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reply_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UserId",
                table: "Topics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_TopicId",
                table: "Reply",
                column: "TopicId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Topics_AspNetUsers_UserId",
            //    table: "Topics",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Topics_AspNetUsers_UserId",
                table: "Topics");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropIndex(
                name: "IX_Topics_UserId",
                table: "Topics");
        }
    }
}
