using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Albayan_Task.Model.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                table: "CustomFields",
                newName: "EnglishKey");

            migrationBuilder.AddColumn<string>(
                name: "ArabicKey",
                table: "CustomFields",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59c3d2bc-d566-4698-8d69-ae54bf77b14a", "AQAAAAIAAYagAAAAEOuNNHUEzds+t6fopLfINYESDoYaD6O6aFXV3e/Bo/oaAp1HfG2gMaixsYLsOAJXfQ==", "d7fd3239-4920-4461-9ea7-322fbc26b18c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArabicKey",
                table: "CustomFields");

            migrationBuilder.RenameColumn(
                name: "EnglishKey",
                table: "CustomFields",
                newName: "Key");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b74ddd14-6340-4840-95c2-db12554843e5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e57dd30-2f5a-410f-ab9f-8b4371c0f794", "AQAAAAIAAYagAAAAEIbBz3Wx2mG7eDclMcjurMlnQ0TSfNRb79nlu0IeAHR5O64hXJGOhVeVY/SQMzhJbA==", "1d28014c-d84c-4481-99cb-457c677533be" });
        }
    }
}
