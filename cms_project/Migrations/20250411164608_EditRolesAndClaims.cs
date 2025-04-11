using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class EditRolesAndClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClaimUserAdminRoleUserAdmin");

            migrationBuilder.DropTable(
                name: "UserAdminAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_Email",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_StudentID",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "StudentID",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "UserAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "UserAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClaimsRole",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimsRole", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimsRole_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_RoleId",
                table: "UserAccounts",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClaimsRole_RolesId",
                table: "ClaimsRole",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Roles_RoleId",
                table: "UserAccounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Roles_RoleId",
                table: "UserAccounts");

            migrationBuilder.DropTable(
                name: "ClaimsRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_RoleId",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserAccounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "UserAccounts");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "UserAccounts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserAccounts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "StudentID",
                table: "UserAccounts",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "UserAccounts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "UserAccounts",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "StudentID");

            migrationBuilder.CreateTable(
                name: "ClaimUserAdminRoleUserAdmin",
                columns: table => new
                {
                    ClaimsId = table.Column<int>(type: "int", nullable: false),
                    RolesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimUserAdminRoleUserAdmin", x => new { x.ClaimsId, x.RolesId });
                    table.ForeignKey(
                        name: "FK_ClaimUserAdminRoleUserAdmin_Claims_ClaimsId",
                        column: x => x.ClaimsId,
                        principalTable: "Claims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClaimUserAdminRoleUserAdmin_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserAdminAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleUserAdminId = table.Column<int>(type: "int", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAdminAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAdminAccounts_Roles_RoleUserAdminId",
                        column: x => x.RoleUserAdminId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_Email",
                table: "UserAccounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_StudentID",
                table: "UserAccounts",
                column: "StudentID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClaimUserAdminRoleUserAdmin_RolesId",
                table: "ClaimUserAdminRoleUserAdmin",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAdminAccounts_RoleUserAdminId",
                table: "UserAdminAccounts",
                column: "RoleUserAdminId");
        }
    }
}
