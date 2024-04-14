using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class absencesTypes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences",
                column: "AbsenceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences",
                column: "AbsenceId",
                unique: true);
        }
    }
}
