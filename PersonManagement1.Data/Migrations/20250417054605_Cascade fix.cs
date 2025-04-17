using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonManagement1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cascadefix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_DateOfBirty_DobId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_AddressId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_DobId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "DobId",
                table: "Persons");

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "DateOfBirty",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonID",
                table: "Addresses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DateOfBirty_PersonId",
                table: "DateOfBirty",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses",
                column: "PersonID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Persons_PersonID",
                table: "Addresses",
                column: "PersonID",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DateOfBirty_Persons_PersonId",
                table: "DateOfBirty",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Persons_PersonID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_DateOfBirty_Persons_PersonId",
                table: "DateOfBirty");

            migrationBuilder.DropIndex(
                name: "IX_DateOfBirty_PersonId",
                table: "DateOfBirty");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PersonID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "DateOfBirty");

            migrationBuilder.DropColumn(
                name: "PersonID",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DobId",
                table: "Persons",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_AddressId",
                table: "Persons",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_DobId",
                table: "Persons",
                column: "DobId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Addresses_AddressId",
                table: "Persons",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_DateOfBirty_DobId",
                table: "Persons",
                column: "DobId",
                principalTable: "DateOfBirty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
