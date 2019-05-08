using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class addfotousuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Usuarios",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Usuarios");
        }
    }
}
