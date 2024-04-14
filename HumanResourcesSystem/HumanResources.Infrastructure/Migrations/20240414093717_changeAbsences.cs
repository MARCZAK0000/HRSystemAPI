using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAbsences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserInfo_UserId",
                table: "Absences");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Absences",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Absences",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Absences_UserInfoId",
                table: "Absences",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_AspNetUsers_UserId",
                table: "Absences",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_UserInfo_UserInfoId",
                table: "Absences",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AspNetUsers_UserId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserInfo_UserInfoId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_UserInfoId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Absences");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Absences",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_UserInfo_UserId",
                table: "Absences",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
