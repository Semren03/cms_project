using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cms_project.Migrations
{
    /// <inheritdoc />
    public partial class assingeduser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentComplaints_Complaints_ComplaintId",
                table: "AttachmentComplaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ComplaintTypes_ComplaintTypeId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_UserAccounts_CreatedBy",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Roles_RoleId",
                table: "UserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints");

            migrationBuilder.RenameTable(
                name: "UserAccounts",
                newName: "UserAccount");

            migrationBuilder.RenameTable(
                name: "Complaints",
                newName: "Complaint");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccounts_RoleId",
                table: "UserAccount",
                newName: "IX_UserAccount_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_CreatedBy",
                table: "Complaint",
                newName: "IX_Complaint_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_ComplaintTypeId",
                table: "Complaint",
                newName: "IX_Complaint_ComplaintTypeId");

            migrationBuilder.AddColumn<int>(
                name: "AssignedTo",
                table: "Complaint",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Complaint",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_AssignedTo",
                table: "Complaint",
                column: "AssignedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_StatusId",
                table: "Complaint",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentComplaints_Complaint_ComplaintId",
                table: "AttachmentComplaints",
                column: "ComplaintId",
                principalTable: "Complaint",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ComplaintTypes_ComplaintTypeId",
                table: "Complaint",
                column: "ComplaintTypeId",
                principalTable: "ComplaintTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Status_StatusId",
                table: "Complaint",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_UserAccount_AssignedTo",
                table: "Complaint",
                column: "AssignedTo",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_UserAccount_CreatedBy",
                table: "Complaint",
                column: "CreatedBy",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccount_Roles_RoleId",
                table: "UserAccount",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttachmentComplaints_Complaint_ComplaintId",
                table: "AttachmentComplaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ComplaintTypes_ComplaintTypeId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Status_StatusId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_UserAccount_AssignedTo",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_UserAccount_CreatedBy",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAccount_Roles_RoleId",
                table: "UserAccount");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_AssignedTo",
                table: "Complaint");

            migrationBuilder.DropIndex(
                name: "IX_Complaint_StatusId",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Complaint");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Complaint");

            migrationBuilder.RenameTable(
                name: "UserAccount",
                newName: "UserAccounts");

            migrationBuilder.RenameTable(
                name: "Complaint",
                newName: "Complaints");

            migrationBuilder.RenameIndex(
                name: "IX_UserAccount_RoleId",
                table: "UserAccounts",
                newName: "IX_UserAccounts_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_CreatedBy",
                table: "Complaints",
                newName: "IX_Complaints_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_ComplaintTypeId",
                table: "Complaints",
                newName: "IX_Complaints_ComplaintTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AttachmentComplaints_Complaints_ComplaintId",
                table: "AttachmentComplaints",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ComplaintTypes_ComplaintTypeId",
                table: "Complaints",
                column: "ComplaintTypeId",
                principalTable: "ComplaintTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_UserAccounts_CreatedBy",
                table: "Complaints",
                column: "CreatedBy",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Roles_RoleId",
                table: "UserAccounts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
