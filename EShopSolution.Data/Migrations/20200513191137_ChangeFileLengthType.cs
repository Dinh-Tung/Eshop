using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("4d48a7a7-10bd-4345-a447-e9fd8c98fd4b"),
                column: "ConcurrencyStamp",
                value: "1cc2437b-59e5-4005-93b5-4e0b15a573ec");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("bad3f12c-da61-44e6-86e7-f67fc4add9e1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1f5aaf40-3702-4bf6-a647-7962e42f469b", "AQAAAAEAACcQAAAAEDPtmCXXACT5wL/47biZ9mEix1zblDTX6Oqb0s7hs0JGso0ajiJvCF0UZkb1T/bfiA==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 5, 14, 2, 11, 36, 214, DateTimeKind.Local).AddTicks(5545));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("4d48a7a7-10bd-4345-a447-e9fd8c98fd4b"),
                column: "ConcurrencyStamp",
                value: "4387a1c8-0e47-4781-aeba-cf89599c2f6b");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("bad3f12c-da61-44e6-86e7-f67fc4add9e1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "839f6ef9-e62a-4bf3-a6ce-1c853227ffff", "AQAAAAEAACcQAAAAELz8SOj+raCB+KKQj1OiQEPvREL9J/9OFetklgE2YaQipDP7Za16rbNHi8mB/XEl4g==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2020, 5, 13, 14, 13, 41, 303, DateTimeKind.Local).AddTicks(3524));
        }
    }
}
