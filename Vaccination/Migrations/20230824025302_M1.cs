using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccination.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    CardName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Create = table.Column<DateTime>(type: "date", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTypes", x => x.Id);
                });
            // Insert data into the CardTypes table
            migrationBuilder.InsertData(
                table: "CardTypes",
                columns: new[] { "Id", "CardName", "Create", "Deleted" },
                values: new object[,]
                {
                    { "EC3FA0E5-66E4-4E80-A460-D139C5BCA1D7", "MOH", DateTime.Now, false },
                    { "14547095-9304-4FA0-BD19-38AA1255985B", "MOD", DateTime.Now, false }
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    ProvinceName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });
            // Insert data into the Provinces table
            migrationBuilder.InsertData(
                table: "Provinces",
                columns: new[] { "Id", "ProvinceName", "Deleted" },
                values: new object[,]
                {
                    { "D7F69CA3-957A-4BB7-B483-C93E7F18486F", "Phnom Penh", false },
                    { "CC4F5999-6141-4185-ADD0-EFFF3FFA5059", "Takeo", false },
                    { "19C33DF6-7D2E-4093-A829-35D7A0B354FF", "Kondal", false },
                    { "97595803-519A-42C9-B6CA-3C05FDBD99DF", "Pursat", false }
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    IdentityId = table.Column<int>(type: "int", maxLength: 10, nullable: false),
                    ProvinceId = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VaccinatedRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Customer_Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    Card_Id = table.Column<string>(type: "varchar(36)", maxLength: 36, nullable: false),
                    DoeseReceived = table.Column<int>(type: "int", nullable: false),
                    DateVaccinated = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VaccinatedRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VaccinatedRecords_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTypes_CardName",
                table: "CardTypes",
                column: "CardName");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ProvinceId",
                table: "Customers",
                column: "ProvinceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_ProvinceName",
                table: "Provinces",
                column: "ProvinceName");

            migrationBuilder.CreateIndex(
                name: "IX_VaccinatedRecords_Customer_Id",
                table: "VaccinatedRecords",
                column: "Customer_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardTypes");

            migrationBuilder.DropTable(
                name: "VaccinatedRecords");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Provinces");
        }
    }
}
