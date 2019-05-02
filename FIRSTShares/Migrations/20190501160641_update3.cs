using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissoes_Cargos_CargoId",
                table: "Permissoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Times_TimeId",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Usuarios",
                newName: "TimeID");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "Usuarios",
                newName: "CargoID");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Usuarios",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_TimeId",
                table: "Usuarios",
                newName: "IX_Usuarios_TimeID");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_CargoId",
                table: "Usuarios",
                newName: "IX_Usuarios_CargoID");

            migrationBuilder.RenameColumn(
                name: "TimeId",
                table: "Times",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "Permissoes",
                newName: "CargoID");

            migrationBuilder.RenameIndex(
                name: "IX_Permissoes_CargoId",
                table: "Permissoes",
                newName: "IX_Permissoes_CargoID");

            migrationBuilder.RenameColumn(
                name: "CargoId",
                table: "Cargos",
                newName: "ID");

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
                name: "FK_Permissoes_Cargos_CargoID",
                table: "Permissoes",
                column: "CargoID",
                principalTable: "Cargos",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissoes_Cargos_CargoID",
                table: "Permissoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Times_TimeID",
                table: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "TimeID",
                table: "Usuarios",
                newName: "TimeId");

            migrationBuilder.RenameColumn(
                name: "CargoID",
                table: "Usuarios",
                newName: "CargoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Usuarios",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_TimeID",
                table: "Usuarios",
                newName: "IX_Usuarios_TimeId");

            migrationBuilder.RenameIndex(
                name: "IX_Usuarios_CargoID",
                table: "Usuarios",
                newName: "IX_Usuarios_CargoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Times",
                newName: "TimeId");

            migrationBuilder.RenameColumn(
                name: "CargoID",
                table: "Permissoes",
                newName: "CargoId");

            migrationBuilder.RenameIndex(
                name: "IX_Permissoes_CargoID",
                table: "Permissoes",
                newName: "IX_Permissoes_CargoId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Cargos",
                newName: "CargoId");

            migrationBuilder.AlterColumn<int>(
                name: "TimeId",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CargoId",
                table: "Usuarios",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Permissoes_Cargos_CargoId",
                table: "Permissoes",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_CargoId",
                table: "Usuarios",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "CargoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Times_TimeId",
                table: "Usuarios",
                column: "TimeId",
                principalTable: "Times",
                principalColumn: "TimeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
