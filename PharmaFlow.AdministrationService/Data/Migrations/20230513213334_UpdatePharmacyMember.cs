using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaFlow.AdministrationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePharmacyMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "pharmacy_member");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "pharmacy_member",
                newName: "LastName");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "pharmacy_member",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member",
                columns: new[] { "ID", "PharmacyID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "pharmacy_member");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "pharmacy_member",
                newName: "Name");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "pharmacy_member",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_pharmacy_member",
                table: "pharmacy_member",
                column: "ID");
        }
    }
}
