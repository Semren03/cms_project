using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class editcomplaintHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintHistory_Complaint_ComplaintId",
                table: "ComplaintHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintHistory_Status_StatusId",
                table: "ComplaintHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplaintHistory",
                table: "ComplaintHistory");

            migrationBuilder.RenameTable(
                name: "ComplaintHistory",
                newName: "ComplaintHistories");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintHistory_StatusId",
                table: "ComplaintHistories",
                newName: "IX_ComplaintHistories_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintHistory_ComplaintId",
                table: "ComplaintHistories",
                newName: "IX_ComplaintHistories_ComplaintId");

            migrationBuilder.AlterColumn<int>(
                name: "actionId",
                table: "ComplaintHistories",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ComplaintHistories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplaintHistories",
                table: "ComplaintHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintHistories_Complaint_ComplaintId",
                table: "ComplaintHistories",
                column: "ComplaintId",
                principalTable: "Complaint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintHistories_Status_StatusId",
                table: "ComplaintHistories",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintHistories_Complaint_ComplaintId",
                table: "ComplaintHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintHistories_Status_StatusId",
                table: "ComplaintHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ComplaintHistories",
                table: "ComplaintHistories");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ComplaintHistories");

            migrationBuilder.RenameTable(
                name: "ComplaintHistories",
                newName: "ComplaintHistory");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintHistories_StatusId",
                table: "ComplaintHistory",
                newName: "IX_ComplaintHistory_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_ComplaintHistories_ComplaintId",
                table: "ComplaintHistory",
                newName: "IX_ComplaintHistory_ComplaintId");

            migrationBuilder.AlterColumn<string>(
                name: "actionId",
                table: "ComplaintHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ComplaintHistory",
                table: "ComplaintHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintHistory_Complaint_ComplaintId",
                table: "ComplaintHistory",
                column: "ComplaintId",
                principalTable: "Complaint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintHistory_Status_StatusId",
                table: "ComplaintHistory",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId");
        }
    }
}
