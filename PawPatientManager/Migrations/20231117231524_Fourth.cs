using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PawPatientManager.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerID",
                table: "Pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerID",
                table: "Pets",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerID",
                table: "Pets",
                column: "OwnerID",
                principalTable: "Owners",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_Owners_OwnerID",
                table: "Pets");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerID",
                table: "Pets",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_Owners_OwnerID",
                table: "Pets",
                column: "OwnerID",
                principalTable: "Owners",
                principalColumn: "ID");
        }
    }
}
