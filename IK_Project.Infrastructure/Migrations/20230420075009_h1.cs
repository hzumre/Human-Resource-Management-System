using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IK_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class h1 : Migration
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

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5542), new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5525) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5551), new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5549) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5556), new DateTime(2023, 4, 20, 10, 50, 8, 984, DateTimeKind.Local).AddTicks(5554) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Menus",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8930), new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8916) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8937), new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8935) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8941), new DateTime(2023, 4, 20, 10, 41, 7, 463, DateTimeKind.Local).AddTicks(8940) });
        }
    }
}
