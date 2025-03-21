using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rush.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationsForTaskEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskEmployees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Tbl_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Tbl_Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Tbl_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Tbl_Projects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskEmployees_Tbl_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tbl_Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_EmployeeId",
                table: "TaskEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_ProjectId",
                table: "TaskEmployees",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_TaskId",
                table: "TaskEmployees",
                column: "TaskId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskEmployees");
        }
    }
}
