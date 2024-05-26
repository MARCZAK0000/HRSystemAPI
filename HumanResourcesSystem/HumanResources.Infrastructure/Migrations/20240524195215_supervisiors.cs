using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class supervisiors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Supervisiors",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    DepramentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supervisiors", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Supervisiors_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Supervisiors_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AdditionalHours",
                columns: table => new
                {
                    AdditionalHoursID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperVisiorID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalHours", x => x.AdditionalHoursID);
                    table.ForeignKey(
                        name: "FK_AdditionalHours_Supervisiors_SuperVisiorID",
                        column: x => x.SuperVisiorID,
                        principalTable: "Supervisiors",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdditionalHours_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalHours_SuperVisiorID",
                table: "AdditionalHours",
                column: "SuperVisiorID");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalHours_UserID",
                table: "AdditionalHours",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Supervisiors_DepartmentsId",
                table: "Supervisiors",
                column: "DepartmentsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdditionalHours");

            migrationBuilder.DropTable(
                name: "Supervisiors");
        }
    }
}
