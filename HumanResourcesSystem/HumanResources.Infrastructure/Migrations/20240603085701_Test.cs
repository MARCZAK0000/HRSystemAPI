using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisiors_Departments_DepramentID",
                table: "Supervisiors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisiors_DepramentID",
                table: "Supervisiors");

            migrationBuilder.AddColumn<int>(
                name: "SuperVisiorDeparmentDepramentID",
                table: "UserInfo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "Supervisiors",
                type: "int",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Supervisiors_DepramentID",
                table: "Supervisiors",
                column: "DepramentID");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_SuperVisiorDeparmentDepramentID",
                table: "UserInfo",
                column: "SuperVisiorDeparmentDepramentID",
                unique: true,
                filter: "[SuperVisiorDeparmentDepramentID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisiors_DepartmentsId",
                table: "Supervisiors",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Supervisiors_Departments_DepartmentsId",
                table: "Supervisiors",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_Supervisiors_SuperVisiorDeparmentDepramentID",
                table: "UserInfo",
                column: "SuperVisiorDeparmentDepramentID",
                principalTable: "Supervisiors",
                principalColumn: "DepramentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Supervisiors_Departments_DepartmentsId",
                table: "Supervisiors");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Supervisiors_SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Supervisiors_DepramentID",
                table: "Supervisiors");

            migrationBuilder.DropIndex(
                name: "IX_Supervisiors_DepartmentsId",
                table: "Supervisiors");

            migrationBuilder.DropColumn(
                name: "SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

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
    }
}
