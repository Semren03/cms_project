using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class ccceditcomplainttype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComplaintType",
                table: "Complaints",
                newName: "ComplaintTypeId");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Complaints",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ComplaintType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplaintType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_ComplaintTypeId",
                table: "Complaints",
                column: "ComplaintTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintType_ComplaintTypeId",
                table: "Complaints",
                column: "ComplaintTypeId",
                principalTable: "ComplaintType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintType_ComplaintTypeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "ComplaintType");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_ComplaintTypeId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Complaints");

            migrationBuilder.RenameColumn(
                name: "ComplaintTypeId",
                table: "Complaints",
                newName: "ComplaintType");
        }
    }
}
