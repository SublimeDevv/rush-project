using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rush.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectionToTables4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Employees_EmployeesId",
                table: "UserTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTasks_Tasks_TaskId",
                table: "UserTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks");

            migrationBuilder.RenameTable(
                name: "UserTasks",
                newName: "EmployeesTasks");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_TaskId",
                table: "EmployeesTasks",
                newName: "IX_EmployeesTasks_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_UserTasks_EmployeesId",
                table: "EmployeesTasks",
                newName: "IX_EmployeesTasks_EmployeesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeesTasks",
                table: "EmployeesTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesTasks_Employees_EmployeesId",
                table: "EmployeesTasks",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeesTasks_Tasks_TaskId",
                table: "EmployeesTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesTasks_Employees_EmployeesId",
                table: "EmployeesTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeesTasks_Tasks_TaskId",
                table: "EmployeesTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeesTasks",
                table: "EmployeesTasks");

            migrationBuilder.RenameTable(
                name: "EmployeesTasks",
                newName: "UserTasks");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesTasks_TaskId",
                table: "UserTasks",
                newName: "IX_UserTasks_TaskId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeesTasks_EmployeesId",
                table: "UserTasks",
                newName: "IX_UserTasks_EmployeesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTasks",
                table: "UserTasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Employees_EmployeesId",
                table: "UserTasks",
                column: "EmployeesId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTasks_Tasks_TaskId",
                table: "UserTasks",
                column: "TaskId",
                principalTable: "Tasks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
