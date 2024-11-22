using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LayeredArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    birth_day = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delete_flg = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "id", "address", "birth_day", "created_at", "created_user", "delete_flg", "first_name", "last_name", "updated_at", "updated_user" },
                values: new object[] { 1, null, new DateTime(2024, 11, 22, 10, 39, 13, 32, DateTimeKind.Local).AddTicks(1595), new DateTime(2024, 11, 22, 10, 39, 13, 34, DateTimeKind.Local).AddTicks(2069), "1", false, "hubert", "do", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    delete_flg = table.Column<bool>(type: "bit", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "id", "created_at", "created_user", "delete_flg", "password", "updated_at", "updated_user", "user_name" },
                values: new object[] { 1, new DateTime(2024, 11, 22, 1, 47, 15, 382, DateTimeKind.Local).AddTicks(9662), "1", false, "password", null, null, "test1" });
        }
    }
}
