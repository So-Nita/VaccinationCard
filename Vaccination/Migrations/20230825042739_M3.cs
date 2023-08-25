using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vaccination.Migrations
{
    /// <inheritdoc />
    public partial class M3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Customers",
            columns: new[] { "Id", "Name", "DOB", "IdentityId", "ProvinceId" },
            values: new object[,]
            {
                { "7334B2A0-63A6-485F-8D65-4678CD5E8E80", "Ty Nara", DateTime.Parse("2000-08-23"), 111111111, "D7F69CA3-957A-4BB7-B483-C93E7F18486F" },
                { "AE852755-EC99-45A5-BF86-881F54B2C5B5", "Keo Thida", DateTime.Parse("1993-01-19"), 231424157, "CC4F5999-6141-4185-ADD0-EFFF3FFA5059" },
                { "A6F32B36-4107-4821-A529-3F05B8653738", "Dy Neith", DateTime.Parse("2003-08-21"), 980967543, "19C33DF6-7D2E-4093-A829-35D7A0B354FF" },
                { "6174FAFC-2E6D-483A-BCF8-37222BCB80E9", "Kem Jena", DateTime.Parse("2002-10-10"), 789034567, "D7F69CA3-957A-4BB7-B483-C93E7F18486F" },
                { "663BC147-60B5-4A48-94C6-9CE80281C485", "Lee Kitty", DateTime.Parse("2001-05-02"), 123452134, "97595803-519A-42C9-B6CA-3C05FDBD99DF" }
            });

            migrationBuilder.InsertData(
            table: "VaccinatedRecords",
            columns: new[] { "Id", "Customer_Id", "Card_Id", "DoeseReceived", "DateVaccinated" },
            values: new object[,]
            {
                { "4F11839E-6AA1-495B-93AB-87212522903C", "7334B2A0-63A6-485F-8D65-4678CD5E8E80", "EC3FA0E5-66E4-4E80-A460-D139C5BCA1D7", 3, DateTime.Parse("2023-08-25") },
                { "E3297B02-778C-4883-8152-CBDEC40A0A8F", "AE852755-EC99-45A5-BF86-881F54B2C5B5", "14547095-9304-4FA0-BD19-38AA1255985B", 2, DateTime.Parse("2023-08-25") },
                { "F0D2A81F-E02D-4B51-BCA8-F377373AB66E", "A6F32B36-4107-4821-A529-3F05B8653738", "EC3FA0E5-66E4-4E80-A460-D139C5BCA1D7", 1, DateTime.Parse("2023-08-25") },
                { "5E703173-7BE1-4E5D-81FE-B3D7565F7D0C", "6174FAFC-2E6D-483A-BCF8-37222BCB80E9", "14547095-9304-4FA0-BD19-38AA1255985B", 3, DateTime.Parse("2023-08-25") },
                { "0172DACB-87F5-412F-BBA8-8C3C9AA73BA5", "663BC147-60B5-4A48-94C6-9CE80281C485", "EC3FA0E5-66E4-4E80-A460-D139C5BCA1D7", 5, DateTime.Parse("2023-08-25") }

            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
