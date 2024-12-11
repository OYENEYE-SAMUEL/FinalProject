using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wastebin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72e8ebcb-e93c-45b9-9844-bd1c81f20059"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name" },
                values: new object[] { new Guid("cdbd9d57-9218-47bc-b749-29ff53574e7e"), new DateTime(2024, 12, 5, 13, 12, 35, 417, DateTimeKind.Utc).AddTicks(4181), true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "RoleId" },
                values: new object[] { new Guid("b2ff105c-18c1-4d2b-9a17-948df7d8d9ff"), "admin@gmail.com", "Admin", true, "$2a$11$sTBHjdpe1Y8OqjAfr6x8jecUDK1B3N5VG3b/GTeewiuH4kQn8IxMe", new Guid("cdbd9d57-9218-47bc-b749-29ff53574e7e") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b2ff105c-18c1-4d2b-9a17-948df7d8d9ff"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("cdbd9d57-9218-47bc-b749-29ff53574e7e"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name" },
                values: new object[] { new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7"), new DateTime(2024, 11, 28, 16, 20, 12, 1, DateTimeKind.Utc).AddTicks(7076), true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "RoleId" },
                values: new object[] { new Guid("72e8ebcb-e93c-45b9-9844-bd1c81f20059"), "admin@gmail.com", "Admin", true, "$2a$11$g6yf9x5Y4QhnDMOIzCw/keQUXpuVHUGAqweJP.n4TpmfUCPe2/VLm", new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7") });
        }
    }
}
