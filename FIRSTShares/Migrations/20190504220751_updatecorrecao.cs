using Microsoft.EntityFrameworkCore.Migrations;

namespace FIRSTShares.Migrations
{
    public partial class updatecorrecao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens");

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_DiscussaoID",
                table: "Postagens",
                column: "DiscussaoID",
                unique: true);
        }
    }
}
