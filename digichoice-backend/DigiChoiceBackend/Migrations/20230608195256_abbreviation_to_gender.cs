using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DigiChoiceBackend.Migrations
{
    /// <inheritdoc />
    public partial class abbreviation_to_gender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Abbreviation",
                table: "PartyMembers",
                newName: "Gender");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "PartyMembers",
                newName: "Abbreviation");
        }
    }
}
