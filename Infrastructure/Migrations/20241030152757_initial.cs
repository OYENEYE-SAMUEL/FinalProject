using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    ProjectDescription = table.Column<string>(type: "longtext", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FundingAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AuthorizedSignature = table.Column<string>(type: "longtext", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    StaffNumber = table.Column<string>(type: "longtext", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrentPlanDateStart = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    CurrentPlanDateEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CurrentPlanType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GovernmentAgents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    SectorName = table.Column<string>(type: "longtext", nullable: false),
                    StateName = table.Column<string>(type: "longtext", nullable: false),
                    ContractId = table.Column<Guid>(type: "char(36)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GovernmentAgents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GovernmentAgents_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    FullName = table.Column<string>(type: "longtext", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoleId = table.Column<Guid>(type: "char(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    TagNumber = table.Column<string>(type: "longtext", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Individuals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Address = table.Column<string>(type: "longtext", nullable: false),
                    Gender = table.Column<string>(type: "longtext", nullable: false),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "char(36)", nullable: false),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Individuals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Individuals_Subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "Subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WasteCollections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StaffId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CommunityId = table.Column<Guid>(type: "char(36)", nullable: true),
                    GovernmentAgentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IndividualId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteCollections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteCollections_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteCollections_GovernmentAgents_GovernmentAgentId",
                        column: x => x.GovernmentAgentId,
                        principalTable: "GovernmentAgents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteCollections_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteCollections_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Staffs",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WasteReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Title = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    WasteCollectionId = table.Column<Guid>(type: "char(36)", nullable: true),
                    CommunityId = table.Column<Guid>(type: "char(36)", nullable: true),
                    GovernmentAgentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IndividualId = table.Column<Guid>(type: "char(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WasteReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WasteReports_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteReports_GovernmentAgents_GovernmentAgentId",
                        column: x => x.GovernmentAgentId,
                        principalTable: "GovernmentAgents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteReports_Individuals_IndividualId",
                        column: x => x.IndividualId,
                        principalTable: "Individuals",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WasteReports_WasteCollections_WasteCollectionId",
                        column: x => x.WasteCollectionId,
                        principalTable: "WasteCollections",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "DateCreated", "IsActive", "Name" },
                values: new object[] { new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1"), new DateTime(2024, 10, 30, 15, 27, 49, 282, DateTimeKind.Utc).AddTicks(8543), true, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FullName", "IsActive", "Password", "RoleId" },
                values: new object[] { new Guid("3cdd30b0-0234-4ce7-b7be-7708d7bb4ad6"), "admin@gmail.com", "Admin", true, "$2a$11$zFlYlj4lxWLKn8kHFLvcLO6PZQzkpPG2RNEE3twqORrWPkV.dmvAW", new Guid("4dd715d0-61b0-4a15-998a-3e3412d438b1") });

            migrationBuilder.CreateIndex(
                name: "IX_Communities_SubscriptionId",
                table: "Communities",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_GovernmentAgents_ContractId",
                table: "GovernmentAgents",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Individuals_SubscriptionId",
                table: "Individuals",
                column: "SubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollections_CommunityId",
                table: "WasteCollections",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollections_GovernmentAgentId",
                table: "WasteCollections",
                column: "GovernmentAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollections_IndividualId",
                table: "WasteCollections",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteCollections_StaffId",
                table: "WasteCollections",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteReports_CommunityId",
                table: "WasteReports",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteReports_GovernmentAgentId",
                table: "WasteReports",
                column: "GovernmentAgentId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteReports_IndividualId",
                table: "WasteReports",
                column: "IndividualId");

            migrationBuilder.CreateIndex(
                name: "IX_WasteReports_WasteCollectionId",
                table: "WasteReports",
                column: "WasteCollectionId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "WasteReports");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "WasteCollections");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropTable(
                name: "GovernmentAgents");

            migrationBuilder.DropTable(
                name: "Individuals");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Subscriptions");
        }
    }
}
