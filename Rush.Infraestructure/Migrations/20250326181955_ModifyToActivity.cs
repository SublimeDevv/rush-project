using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rush.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ModifyToActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Activities_Tbl_Employees_EmployeeId",
                table: "Tbl_Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Tbl_Activities",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Activities_Tbl_Employees_EmployeeId",
                table: "Tbl_Activities",
                column: "EmployeeId",
                principalTable: "Tbl_Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Activities_Tbl_Employees_EmployeeId",
                table: "Tbl_Activities");

            migrationBuilder.AlterColumn<Guid>(
                name: "EmployeeId",
                table: "Tbl_Activities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Activities_Tbl_Employees_EmployeeId",
                table: "Tbl_Activities",
                column: "EmployeeId",
                principalTable: "Tbl_Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
