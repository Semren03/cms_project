using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class complainthistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComplaintHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    actionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComplaintId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    resolverID = table.Column<int>(type: "int", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComplaintHistory_Complaint_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaint",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplaintHistory_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintHistory_ComplaintId",
                table: "ComplaintHistory",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintHistory_StatusId",
                table: "ComplaintHistory",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplaintHistory");
        }
    }
}
