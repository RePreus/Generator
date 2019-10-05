using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Generator.Application.Migrations
{
    public partial class userchoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChosenPictureId = table.Column<Guid>(nullable: false),
                    OtherPictureId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChoices", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChoices");
        }
    }
}
