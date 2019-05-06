using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class categoriapost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussaoID",
                table: "Postagens",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CategoriaID",
                table: "Postagens",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_CategoriaID",
                table: "Postagens",
                column: "CategoriaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Categorias_CategoriaID",
                table: "Postagens",
                column: "CategoriaID",
                principalTable: "Categorias",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                principalTable: "Discussoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Categorias_CategoriaID",
                table: "Postagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_CategoriaID",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "CategoriaID",
                table: "Postagens");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussaoID",
                table: "Postagens",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                principalTable: "Discussoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
