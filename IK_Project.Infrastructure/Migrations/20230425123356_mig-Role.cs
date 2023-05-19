using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IK_Project.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("459b4b8d-035a-4d9f-a2cc-b9366f955e0f"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("4c370c61-dd64-41f2-af84-f7c27d68d9eb"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("93b6bb5a-0d79-4299-8b20-e1b9413318a4"));

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Menus",
                type: "varchar(64)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { new Guid("0127c4a0-87cc-43de-bf1f-9e48fe4b295e"), null, "Admin", new DateTime(2023, 4, 25, 15, 33, 56, 469, DateTimeKind.Local).AddTicks(8563), null, null, "CompanyManager", "COMPANYMANAGER", 1 },
                    { new Guid("f289c038-cd2c-4c1f-923d-93a7b13a36fd"), null, "Admin", new DateTime(2023, 4, 25, 15, 33, 56, 469, DateTimeKind.Local).AddTicks(8566), null, null, "Employee", "EMPLOYEE", 1 },
                    { new Guid("f7d190c3-0553-416d-b2ec-85cfd907c3aa"), null, "Admin", new DateTime(2023, 4, 25, 15, 33, 56, 469, DateTimeKind.Local).AddTicks(8546), null, null, "Admin", "ADMIN", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1510), new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1506) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1516), new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1514) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1520), new DateTime(2023, 4, 25, 15, 33, 56, 470, DateTimeKind.Local).AddTicks(1519) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("0127c4a0-87cc-43de-bf1f-9e48fe4b295e"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f289c038-cd2c-4c1f-923d-93a7b13a36fd"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d190c3-0553-416d-b2ec-85cfd907c3aa"));

            migrationBuilder.AlterColumn<string>(
                name: "Period",
                table: "Menus",
                type: "varchar",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { new Guid("459b4b8d-035a-4d9f-a2cc-b9366f955e0f"), null, "Admin", new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(5565), null, null, "Admin", "ADMIN", 1 },
                    { new Guid("4c370c61-dd64-41f2-af84-f7c27d68d9eb"), null, "Admin", new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(5610), null, null, "Employee", "EMPLOYEE", 1 },
                    { new Guid("93b6bb5a-0d79-4299-8b20-e1b9413318a4"), null, "Admin", new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(5606), null, null, "CompanyManager", "COMPANYMANAGER", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9015), new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9009) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9023), new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9022) });

            migrationBuilder.UpdateData(
                table: "Menus",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ReleaseDate" },
                values: new object[] { new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9029), new DateTime(2023, 4, 25, 15, 30, 43, 74, DateTimeKind.Local).AddTicks(9027) });
        }
    }
}
