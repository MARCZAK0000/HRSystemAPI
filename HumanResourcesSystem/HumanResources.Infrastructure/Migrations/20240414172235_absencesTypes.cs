using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class absencesTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AbsenceType",
                table: "Absences",
                newName: "AbsencesId");

            migrationBuilder.AddColumn<int>(
                name: "AbsenceId",
                table: "Absences",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AbsencesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsencesTypes", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Absences_AbsencesTypes_AbsencesId",
                table: "Absences");

            migrationBuilder.DropTable(
                name: "AbsencesTypes");

            migrationBuilder.DropIndex(
                name: "IX_Absences_AbsencesId",
                table: "Absences");

            migrationBuilder.DropColumn(
                name: "AbsenceId",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "AbsencesId",
                table: "Absences",
                newName: "AbsenceType");
        }
    }
}
