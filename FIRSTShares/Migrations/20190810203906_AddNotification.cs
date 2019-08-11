using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class AddNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioNotificadoID = table.Column<int>(nullable: true),
                    UsuarioAcaoID = table.Column<int>(nullable: true),
                    Acao = table.Column<int>(nullable: false),
                    Excluido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_UsuarioAcaoID",
                        column: x => x.UsuarioAcaoID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Notificacoes_Usuarios_UsuarioNotificadoID",
                        column: x => x.UsuarioNotificadoID,
                        principalTable: "Usuarios",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_UsuarioAcaoID",
                table: "Notificacoes",
                column: "UsuarioAcaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Notificacoes_UsuarioNotificadoID",
                table: "Notificacoes",
                column: "UsuarioNotificadoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notificacoes");
        }
    }
}
