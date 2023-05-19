using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IK_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v88 : Migration
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
                values: new object[] { new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(586), new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(569) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(590), new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(589) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(593), new DateTime(2023, 4, 19, 14, 56, 42, 215, DateTimeKind.Local).AddTicks(593) });
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
                values: new object[] { new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7019), new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(6986) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7027), new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7025) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7033), new DateTime(2023, 4, 19, 12, 49, 46, 595, DateTimeKind.Local).AddTicks(7032) });
        }
    }
}
