using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Arrivalsv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Arrivals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Arrivals",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Arrivals_UserId",
                table: "Arrivals",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_AspNetUsers_UserId",
                table: "Arrivals",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_AspNetUsers_UserId",
                table: "Arrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals");

            migrationBuilder.DropIndex(
                name: "IX_Arrivals_UserId",
                table: "Arrivals");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Arrivals");

            migrationBuilder.AlterColumn<int>(
                name: "UserInfoId",
                table: "Arrivals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
