using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaFlow.AdministrationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePharmacyMemberPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member",
                column: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member",
                columns: new[] { "ID", "PharmacyID" });
        }
    }
}
