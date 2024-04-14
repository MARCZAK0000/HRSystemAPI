using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeAbsences2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AspNetUsers_UserId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserInfo_UserInfoId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_Arrivals_UserInfoId",
                table: "Arrivals");

            migrationBuilder.DropIndex(
                name: "IX_Absences_UserInfoId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Arrivals");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Absences");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "UserInfo",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserInfoUserId",
                table: "Arrivals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId1",
                table: "UserInfo",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Arrivals_UserInfoUserId",
                table: "Arrivals",
                column: "UserInfoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_UserInfo_UserId",
                table: "Absences",
                column: "UserId",
                principalTable: "UserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoUserId",
                table: "Arrivals",
                column: "UserInfoUserId",
                principalTable: "UserInfo",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId1",
                table: "UserInfo",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_UserInfo_UserId",
                table: "Absences");

            migrationBuilder.DropForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoUserId",
                table: "Arrivals");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId1",
                table: "UserInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_UserId1",
                table: "UserInfo");

            migrationBuilder.DropIndex(
                name: "IX_Arrivals_UserInfoUserId",
                table: "Arrivals");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "UserInfoUserId",
                table: "Arrivals");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserInfo",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Arrivals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserInfoId",
                table: "Absences",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInfo",
                table: "UserInfo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_UserId",
                table: "UserInfo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Arrivals_UserInfoId",
                table: "Arrivals",
                column: "UserInfoId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Arrivals_UserInfo_UserInfoId",
                table: "Arrivals",
                column: "UserInfoId",
                principalTable: "UserInfo",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_AspNetUsers_UserId",
                table: "UserInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
