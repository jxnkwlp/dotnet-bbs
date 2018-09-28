using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleBBS.Web.Data.Migrations
{
    public partial class alter_topic_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Guid",
            //    table: "Topics");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Reply_UserId",
            //    table: "Reply",
            //    column: "UserId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Reply_AspNetUsers_UserId",
            //    table: "Reply",
            //    column: "UserId",
            //    principalTable: "AspNetUsers",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Reply_AspNetUsers_UserId",
            //    table: "Reply");

            //migrationBuilder.DropIndex(
            //    name: "IX_Reply_UserId",
            //    table: "Reply");

            //migrationBuilder.AddColumn<Guid>(
            //    name: "Guid",
            //    table: "Topics",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
