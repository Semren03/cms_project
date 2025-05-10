using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class Complainttypecatigores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ComplaintTypeResolverId",
                table: "UserAccount",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccount_ComplaintTypeResolverId",
                table: "UserAccount",
                column: "ComplaintTypeResolverId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_ComplaintTypes_ComplaintTypeResolverId",
                table: "UserAccount",
                column: "ComplaintTypeResolverId",
                principalTable: "ComplaintTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_ComplaintTypes_ComplaintTypeResolverId",
                table: "UserAccount");

            migrationBuilder.DropIndex(
                name: "IX_UserAccount_ComplaintTypeResolverId",
                table: "UserAccount");

            migrationBuilder.DropColumn(
                name: "ComplaintTypeResolverId",
                table: "UserAccount");
        }
    }
}
