using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class EditAttachmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentComplaint_Complaints_ComplaintId",
                table: "AttachmentComplaint");

            migrationBuilder.DropColumn(
                name: "CompaintId",
                table: "AttachmentComplaint");

            migrationBuilder.AlterColumn<int>(
                name: "ComplaintId",
                table: "AttachmentComplaint",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentComplaint_Complaints_ComplaintId",
                table: "AttachmentComplaint",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentComplaint_Complaints_ComplaintId",
                table: "AttachmentComplaint");

            migrationBuilder.AlterColumn<int>(
                name: "ComplaintId",
                table: "AttachmentComplaint",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CompaintId",
                table: "AttachmentComplaint",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentComplaint_Complaints_ComplaintId",
                table: "AttachmentComplaint",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id");
        }
    }
}
