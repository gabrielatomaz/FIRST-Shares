using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class fotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Fotos_FotoID",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FotoID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "FotoID",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FotoID",
                table: "Usuarios",
                column: "FotoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Fotos_FotoID",
                table: "Usuarios",
                column: "FotoID",
                principalTable: "Fotos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Fotos_FotoID",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FotoID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "FotoID",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FotoID",
                table: "Usuarios",
                column: "FotoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Fotos_FotoID",
                table: "Usuarios",
                column: "FotoID",
                principalTable: "Fotos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
