using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removeSupervisior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalHours_Supervisiors_SuperVisiorID",
                table: "AdditionalHours");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfo_Supervisiors_SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

            migrationBuilder.DropTable(
                name: "Supervisiors");

            migrationBuilder.DropIndex(
                name: "IX_UserInfo_SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "SuperVisiorDeparmentDepramentID",
                table: "UserInfo");

            migrationBuilder.AddColumn<bool>(
                name: "IsSupervisior",
                table: "UserInfo",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_AdditionalHours_UserInfo_SuperVisiorID",
                table: "AdditionalHours",
                column: "SuperVisiorID",
                principalTable: "UserInfo",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdditionalHours_UserInfo_SuperVisiorID",
                table: "AdditionalHours");

            migrationBuilder.DropColumn(
                name: "IsSupervisior",
                table: "UserInfo");

            migrationBuilder.AddColumn<int>(
                name: "SuperVisiorDeparmentDepramentID",
                table: "UserInfo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Supervisiors",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentsId = table.Column<int>(type: "int", nullable: true),
                    DepramentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisiors", x => x.UserID);
                    table.UniqueConstraint("AK_Supervisiors_DepramentID", x => x.DepramentID);
                    table.ForeignKey(
                        name: "FK_Supervisiors_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Supervisiors_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "FK_AdditionalHours_Supervisiors_SuperVisiorID",
                table: "AdditionalHours",
                column: "SuperVisiorID",
                principalTable: "Supervisiors",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfo_Supervisiors_SuperVisiorDeparmentDepramentID",
                table: "UserInfo",
                column: "SuperVisiorDeparmentDepramentID",
                principalTable: "Supervisiors",
                principalColumn: "DepramentID");
        }
    }
}
