using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class AddPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Discussoes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Assunto = table.Column<string>(nullable: true),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussoes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Postagens",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DiscussaoID = table.Column<int>(nullable: true),
                    Conteudo = table.Column<string>(nullable: true),
                    UsuarioID = table.Column<int>(nullable: true),
                    IDPostagemPaiID = table.Column<int>(nullable: true),
                    PostagemOficial = table.Column<bool>(nullable: false),
                    DataCriacao = table.Column<DateTime>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagens", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Postagens_Discussoes_DiscussaoID",
                        column: x => x.DiscussaoID,
                        principalTable: "Discussoes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postagens_Postagens_IDPostagemPaiID",
                        column: x => x.IDPostagemPaiID,
                        principalTable: "Postagens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Postagens_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Curtidas",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioID = table.Column<int>(nullable: true),
                    PostagemID = table.Column<int>(nullable: true),
                    Curtiu = table.Column<bool>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false),
                    PostagemID1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curtidas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Curtidas_Postagens_PostagemID",
                        column: x => x.PostagemID,
                        principalTable: "Postagens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curtidas_Postagens_PostagemID1",
                        column: x => x.PostagemID1,
                        principalTable: "Postagens",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Curtidas_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_PostagemID",
                table: "Curtidas",
                column: "PostagemID");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_PostagemID1",
                table: "Curtidas",
                column: "PostagemID1");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_UsuarioID",
                table: "Curtidas",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_IDPostagemPaiID",
                table: "Postagens",
                column: "IDPostagemPaiID");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_UsuarioID",
                table: "Postagens",
                column: "UsuarioID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "Curtidas");

            migrationBuilder.DropTable(
                name: "Postagens");

            migrationBuilder.DropTable(
                name: "Discussoes");
        }
    }
}
