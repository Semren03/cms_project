using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class EditUserAdminRoleClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAdminAccounts_Roles_RoleUserAdminId",
                table: "UserAdminAccounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserAdminAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "RoleUserAdminId",
                table: "UserAdminAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdminAccounts_Roles_RoleUserAdminId",
                table: "UserAdminAccounts",
                column: "RoleUserAdminId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAdminAccounts_Roles_RoleUserAdminId",
                table: "UserAdminAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "RoleUserAdminId",
                table: "UserAdminAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserAdminAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAdminAccounts_Roles_RoleUserAdminId",
                table: "UserAdminAccounts",
                column: "RoleUserAdminId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
