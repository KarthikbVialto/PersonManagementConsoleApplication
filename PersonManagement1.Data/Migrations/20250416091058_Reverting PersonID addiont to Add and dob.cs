using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement1.Data.Migrations
{
    /// <inheritdoc />
    public partial class RevertingPersonIDaddionttoAddanddob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "DateOfBirty");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Addresses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "DateOfBirty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
