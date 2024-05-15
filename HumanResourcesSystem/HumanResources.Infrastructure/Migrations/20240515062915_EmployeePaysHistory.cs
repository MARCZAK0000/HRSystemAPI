using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeePaysHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeRateEuro",
                table: "EmployeePays");

            migrationBuilder.DropColumn(
                name: "ExchangeRateUSD",
                table: "EmployeePays");

            migrationBuilder.CreateTable(
                name: "EmployeePayHistory",
                columns: table => new
                {
                    EmployeePayHistoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmpolyeePayID = table.Column<int>(type: "int", nullable: false),
                    MonthPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthPaymentEuro = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MonthPaymentUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreateDays = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiededDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayHistory", x => x.EmployeePayHistoryID);
                    table.ForeignKey(
                        name: "FK_EmployeePayHistory_EmployeePays_EmpolyeePayID",
                        column: x => x.EmpolyeePayID,
                        principalTable: "EmployeePays",
                        principalColumn: "EmployeePayID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayHistory_UserInfo_UserID",
                        column: x => x.UserID,
                        principalTable: "UserInfo",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameFrom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRates", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayHistory_EmpolyeePayID",
                table: "EmployeePayHistory",
                column: "EmpolyeePayID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayHistory_UserID",
                table: "EmployeePayHistory",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeePayHistory");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateEuro",
                table: "EmployeePays",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ExchangeRateUSD",
                table: "EmployeePays",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
