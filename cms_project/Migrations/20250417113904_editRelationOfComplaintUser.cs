using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class editRelationOfComplaintUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_UserAccounts_UserAccountId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_UserAccountId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "UserAccountId",
                table: "Complaints");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_CreatedBy",
                table: "Complaints",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_UserAccounts_CreatedBy",
                table: "Complaints",
                column: "CreatedBy",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_UserAccounts_CreatedBy",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_CreatedBy",
                table: "Complaints");

            migrationBuilder.AddColumn<int>(
                name: "UserAccountId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_UserAccountId",
                table: "Complaints",
                column: "UserAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_UserAccounts_UserAccountId",
                table: "Complaints",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }
    }
}
