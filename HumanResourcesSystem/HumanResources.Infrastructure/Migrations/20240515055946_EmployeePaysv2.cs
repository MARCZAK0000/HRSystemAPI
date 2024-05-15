using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeePaysv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rate",
                table: "EmployeePays",
                newName: "RatePLN");

            migrationBuilder.RenameColumn(
                name: "EuroRate",
                table: "EmployeePays",
                newName: "RateUSD");

            migrationBuilder.AddColumn<decimal>(
                name: "RateEURO",
                table: "EmployeePays",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateEURO",
                table: "EmployeePays");

            migrationBuilder.RenameColumn(
                name: "RateUSD",
                table: "EmployeePays",
                newName: "EuroRate");

            migrationBuilder.RenameColumn(
                name: "RatePLN",
                table: "EmployeePays",
                newName: "Rate");
        }
    }
}
