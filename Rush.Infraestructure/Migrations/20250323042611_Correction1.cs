using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rush.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Correction1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskEmployees_Tbl_Projects_ProjectId",
                table: "TaskEmployees");

            migrationBuilder.DropIndex(
                name: "IX_TaskEmployees_ProjectId",
                table: "TaskEmployees");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskEmployees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProjectId",
                table: "TaskEmployees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskEmployees_ProjectId",
                table: "TaskEmployees",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskEmployees_Tbl_Projects_ProjectId",
                table: "TaskEmployees",
                column: "ProjectId",
                principalTable: "Tbl_Projects",
                principalColumn: "Id");
        }
    }
}
