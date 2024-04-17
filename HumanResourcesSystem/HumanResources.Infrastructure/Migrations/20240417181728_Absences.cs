using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HumanResources.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Absences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isAccepted",
                table: "Absences",
                newName: "IsAccepted");

            migrationBuilder.AddColumn<bool>(
                name: "Declined",
                table: "Absences",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Declined",
                table: "Absences");

            migrationBuilder.RenameColumn(
                name: "IsAccepted",
                table: "Absences",
                newName: "isAccepted");
        }
    }
}
