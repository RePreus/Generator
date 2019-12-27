using Microsoft.EntityFrameworkCore.Migrations;

namespace Generator.Identity.Migrations
{
    public partial class ChangeVariableTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GoogleId",
                table: "Users",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GoogleId",
                table: "Users",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: false);
        }
    }
}
