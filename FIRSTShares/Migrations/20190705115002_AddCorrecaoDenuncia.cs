using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class AddCorrecaoDenuncia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncia_Usuarios_UsuarioDenunciadoID",
                table: "Denuncia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denuncia",
                table: "Denuncia");

            migrationBuilder.RenameTable(
                name: "Denuncia",
                newName: "Denuncias");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncia_UsuarioDenunciadoID",
                table: "Denuncias",
                newName: "IX_Denuncias_UsuarioDenunciadoID");

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "Denuncias",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denuncias",
                table: "Denuncias",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Usuarios_UsuarioDenunciadoID",
                table: "Denuncias",
                column: "UsuarioDenunciadoID",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Usuarios_UsuarioDenunciadoID",
                table: "Denuncias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Denuncias",
                table: "Denuncias");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "Denuncias");

            migrationBuilder.RenameTable(
                name: "Denuncias",
                newName: "Denuncia");

            migrationBuilder.RenameIndex(
                name: "IX_Denuncias_UsuarioDenunciadoID",
                table: "Denuncia",
                newName: "IX_Denuncia_UsuarioDenunciadoID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Denuncia",
                table: "Denuncia",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncia_Usuarios_UsuarioDenunciadoID",
                table: "Denuncia",
                column: "UsuarioDenunciadoID",
                principalTable: "Usuarios",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
