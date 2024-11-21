using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LayeredArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 22, 1, 46, 59, 140, DateTimeKind.Local).AddTicks(8528));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "id",
                keyValue: 1,
                column: "created_at",
                value: new DateTime(2024, 11, 22, 1, 43, 38, 752, DateTimeKind.Local).AddTicks(9375));
        }
    }
}
