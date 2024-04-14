using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class absencesTypes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AbsencesTypes_AbsencesId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsencesId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "AbsencesId",
                table: "Absences");

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences",
                column: "AbsenceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_AbsencesTypes_AbsenceId",
                table: "Absences",
                column: "AbsenceId",
                principalTable: "AbsencesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AbsencesTypes_AbsenceId",
                table: "Absences");

            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsenceId",
                table: "Absences");

            migrationBuilder.AddColumn<int>(
                name: "AbsencesId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Absences_AbsencesId",
                table: "Absences",
                column: "AbsencesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Absences_AbsencesTypes_AbsencesId",
                table: "Absences",
                column: "AbsencesId",
                principalTable: "AbsencesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
