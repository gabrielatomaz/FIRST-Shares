using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Times_TimeID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "TimeID",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoID",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "Cargos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Times_TimeID",
                table: "Usuarios",
                column: "TimeID",
                principalTable: "Times",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Times_TimeID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "TimeID",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "CargoID",
                table: "Usuarios",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "Cargos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Times_TimeID",
                table: "Usuarios",
                column: "TimeID",
                principalTable: "Times",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
