using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisiors_Departments_DepartmentsId",
                table: "Supervisiors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisiors_DepartmentsId",
                table: "Supervisiors");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "Supervisiors");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisiors_DepramentID",
                table: "Supervisiors",
                column: "DepramentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisiors_Departments_DepramentID",
                table: "Supervisiors",
                column: "DepramentID",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisiors_Departments_DepramentID",
                table: "Supervisiors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisiors_DepramentID",
                table: "Supervisiors");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Supervisiors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Supervisiors_DepartmentsId",
                table: "Supervisiors",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisiors_Departments_DepartmentsId",
                table: "Supervisiors",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
