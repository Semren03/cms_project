using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class editcomplaintstatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "resolverID",
                table: "ComplaintHistory");

            migrationBuilder.RenameColumn(
                name: "actionId",
                table: "ComplaintHistory",
                newName: "ResolverName");

            migrationBuilder.AddColumn<string>(
                name: "ActionStatus",
                table: "ComplaintHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionStatus",
                table: "ComplaintHistory");

            migrationBuilder.RenameColumn(
                name: "ResolverName",
                table: "ComplaintHistory",
                newName: "actionId");

            migrationBuilder.AddColumn<int>(
                name: "resolverID",
                table: "ComplaintHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
