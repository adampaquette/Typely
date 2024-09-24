using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sample.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "FirstName", "LastName", "Phone", "PhoneType" },
                values: new object[,]
                {
                    { 1, "John", "Doe", "123-456-7890", 1 },
                    { 2, "Jane", "Smith", "987-654-3210", 3 }
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "ContactId", "Id", "City", "CivicNumber", "State", "Street", "ZipCode" },
                values: new object[,]
                {
                    { 1, 2, "Other City", 456, "NY", "Oak Ave", "67890" },
                    { 2, 1, "Anytown", 123, "CA", "Main St", "12345" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumns: new[] { "ContactId", "Id" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumns: new[] { "ContactId", "Id" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
