using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class updatenovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Curtidas_Postagens_PostagemID1",
                table: "Curtidas");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Postagens_IDPostagemPaiID",
                table: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Curtidas_PostagemID1",
                table: "Curtidas");

            migrationBuilder.DropColumn(
                name: "PostagemID1",
                table: "Curtidas");

            migrationBuilder.RenameColumn(
                name: "IDPostagemPaiID",
                table: "Postagens",
                newName: "PostagemPaiID");

            migrationBuilder.RenameIndex(
                name: "IX_Postagens_IDPostagemPaiID",
                table: "Postagens",
                newName: "IX_Postagens_PostagemPaiID");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussaoID",
                table: "Postagens",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                principalTable: "Discussoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Postagens_PostagemPaiID",
                table: "Postagens",
                column: "PostagemPaiID",
                principalTable: "Postagens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens");

            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Postagens_PostagemPaiID",
                table: "Postagens");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens");

            migrationBuilder.RenameColumn(
                name: "PostagemPaiID",
                table: "Postagens",
                newName: "IDPostagemPaiID");

            migrationBuilder.RenameIndex(
                name: "IX_Postagens_PostagemPaiID",
                table: "Postagens",
                newName: "IX_Postagens_IDPostagemPaiID");

            migrationBuilder.AlterColumn<int>(
                name: "DiscussaoID",
                table: "Postagens",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PostagemID1",
                table: "Curtidas",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID");

            migrationBuilder.CreateIndex(
                name: "IX_Curtidas_PostagemID1",
                table: "Curtidas",
                column: "PostagemID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Curtidas_Postagens_PostagemID1",
                table: "Curtidas",
                column: "PostagemID1",
                principalTable: "Postagens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Discussoes_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                principalTable: "Discussoes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Postagens_IDPostagemPaiID",
                table: "Postagens",
                column: "IDPostagemPaiID",
                principalTable: "Postagens",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
