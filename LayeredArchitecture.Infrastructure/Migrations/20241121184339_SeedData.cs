using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LayeredArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_user = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    delete_flg = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "id", "created_at", "created_user", "delete_flg", "password", "updated_at", "updated_user", "user_name" },
                values: new object[] { 1, new DateTime(2024, 11, 22, 1, 43, 38, 752, DateTimeKind.Local).AddTicks(9375), "1", false, "password", null, null, "test1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
