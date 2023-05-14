using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaFlow.AdministrationService.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIDToPharmacyMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "pharmacy_member",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserID",
                table: "pharmacy_member");
        }
    }
}
