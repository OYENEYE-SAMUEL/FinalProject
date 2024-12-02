using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class wastemore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Subscriptions_SubscriptionId",
                table: "Communities");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("3cdd30b0-0234-4ce7-b7be-7708d7bb4ad6"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1"));

            migrationBuilder.AddColumn<string>(
                name: "ReferenceNumber",
                table: "Payments",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "Payments",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionId",
                table: "Communities",
                type: "char(36)",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "char(36)");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name" },
                values: new object[] { new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7"), new DateTime(2024, 11, 28, 16, 20, 12, 1, DateTimeKind.Utc).AddTicks(7076), true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "RoleId" },
                values: new object[] { new Guid("72e8ebcb-e93c-45b9-9844-bd1c81f20059"), "admin@gmail.com", "Admin", true, "$2a$11$g6yf9x5Y4QhnDMOIzCw/keQUXpuVHUGAqweJP.n4TpmfUCPe2/VLm", new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7") });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Subscriptions_SubscriptionId",
                table: "Communities",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Subscriptions_SubscriptionId",
                table: "Payments",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Communities_Subscriptions_SubscriptionId",
                table: "Communities");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Subscriptions_SubscriptionId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SubscriptionId",
                table: "Payments");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("72e8ebcb-e93c-45b9-9844-bd1c81f20059"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("07163a41-30b2-472f-bdfb-040ac334c4a7"));

            migrationBuilder.DropColumn(
                name: "ReferenceNumber",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "Payments");

            migrationBuilder.AlterColumn<Guid>(
                name: "SubscriptionId",
                table: "Communities",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name" },
                values: new object[] { new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1"), new DateTime(2024, 10, 30, 15, 27, 49, 282, DateTimeKind.Utc).AddTicks(8543), true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "RoleId" },
                values: new object[] { new Guid("3cdd30b0-0234-4ce7-b7be-7708d7bb4ad6"), "admin@gmail.com", "Admin", true, "$2a$11$zFlYlj4lxWLKn8kHFLvcLO6PZQzkpPG2RNEE3twqORrWPkV.dmvAW", new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1") });

            migrationBuilder.AddForeignKey(
                name: "FK_Communities_Subscriptions_SubscriptionId",
                table: "Communities",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
