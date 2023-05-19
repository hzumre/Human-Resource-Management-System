using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IK_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migs : Migration
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
                values: new object[] { new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(630), new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(618) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(636), new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(635) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(640), new DateTime(2023, 4, 25, 13, 28, 5, 109, DateTimeKind.Local).AddTicks(639) });
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
                values: new object[] { new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7476), new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7453) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7483), new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7481) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7489), new DateTime(2023, 4, 23, 21, 40, 1, 575, DateTimeKind.Local).AddTicks(7488) });
        }
    }
}
