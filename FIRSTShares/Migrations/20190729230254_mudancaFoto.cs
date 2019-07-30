using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class mudancaFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "FotoID",
                table: "Usuarios",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Fotos",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FotoBase64 = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fotos", x => x.ID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Fotos_FotoID",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Fotos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_FotoID",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "FotoID",
                table: "Usuarios");

            migrationBuilder.AddColumn<string>(
                name: "Foto",
                table: "Usuarios",
                nullable: true);
        }
    }
}
