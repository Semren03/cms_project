using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class addattachmenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentComplaint",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompaintId = table.Column<int>(type: "int", nullable: false),
                    ComplaintId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentComplaint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentComplaint_Complaints_ComplaintId",
                        column: x => x.ComplaintId,
                        principalTable: "Complaints",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentComplaint_ComplaintId",
                table: "AttachmentComplaint",
                column: "ComplaintId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentComplaint");
        }
    }
}
