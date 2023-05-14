using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaFlow.AdministrationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddStateToPharmacyAndMemberAndType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "pharmacy_member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "pharmacy",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "pharmacy_member");

            migrationBuilder.DropColumn(
                name: "State",
                table: "pharmacy");
        }
    }
}
