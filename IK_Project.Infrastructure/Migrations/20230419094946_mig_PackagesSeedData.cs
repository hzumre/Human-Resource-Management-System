using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IK_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_PackagesSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Menus",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "Currecy", "DeletedDate", "ModifiedBy", "ModifiedDate", "Name", "Period", "ReleaseDate", "Status", "UnitPrice", "UserAmount" },
                values: new object[,]
                {
                    { 1, "Admin", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7019), 0, null, null, null, "Basic", "3", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(6986), 1, 1000m, 100 },
                    { 2, "Admin", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7027), 0, null, null, null, "Standart", "3", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7025), 1, 2000m, 200 },
                    { 3, "Admin", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7033), 0, null, null, null, "Premium", "3", new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7032), 1, 3000m, 300 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Menus",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);
        }
    }
}
