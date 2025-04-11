using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class complaitnattachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_attachmentComplaints_Complaints_ComplaintId",
                table: "attachmentComplaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_attachmentComplaints",
                table: "attachmentComplaints");

            migrationBuilder.RenameTable(
                name: "attachmentComplaints",
                newName: "AttachmentComplaints");

            migrationBuilder.RenameIndex(
                name: "IX_attachmentComplaints_ComplaintId",
                table: "AttachmentComplaints",
                newName: "IX_AttachmentComplaints_ComplaintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AttachmentComplaints",
                table: "AttachmentComplaints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentComplaints_Complaints_ComplaintId",
                table: "AttachmentComplaints",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentComplaints_Complaints_ComplaintId",
                table: "AttachmentComplaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AttachmentComplaints",
                table: "AttachmentComplaints");

            migrationBuilder.RenameTable(
                name: "AttachmentComplaints",
                newName: "attachmentComplaints");

            migrationBuilder.RenameIndex(
                name: "IX_AttachmentComplaints_ComplaintId",
                table: "attachmentComplaints",
                newName: "IX_attachmentComplaints_ComplaintId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_attachmentComplaints",
                table: "attachmentComplaints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_attachmentComplaints_Complaints_ComplaintId",
                table: "attachmentComplaints",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
