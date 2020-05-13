using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 5, 11, 2, 39, 39, 421, DateTimeKind.Local).AddTicks(1424));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    productId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_productId",
                        column: x => x.productId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_productId",
                table: "ProductImages",
                column: "productId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 5, 11, 2, 39, 39, 421, DateTimeKind.Local).AddTicks(1424),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRole",
                keyColumn: "Id",
                keyValue: new Guid("4d48a7a7-10bd-4345-a447-e9fd8c98fd4b"),
                column: "ConcurrencyStamp",
                value: "57e09bbb-3928-4f59-9230-9bedb89eea24");

            migrationBuilder.UpdateData(
                table: "AppUser",
                keyColumn: "Id",
                keyValue: new Guid("bad3f12c-da61-44e6-86e7-f67fc4add9e1"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e5bd32ea-5123-40dd-a6ce-cffa2326016d", "AQAAAAEAACcQAAAAEGGKBPzkmq/MlbC/xtEsebNwShkmDOG/nTyaIDmWYNEz9pvoh7bk+EB8wGtNnA0j3w==" });

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
                value: new DateTime(2020, 5, 11, 2, 39, 39, 441, DateTimeKind.Local).AddTicks(429));
        }
    }
}
