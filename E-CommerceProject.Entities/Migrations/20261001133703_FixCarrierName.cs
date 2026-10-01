using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceProject.Entities.Migrations
{
    /// <inheritdoc />
    public partial class FixCarrierName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Carrieer",
                table: "Shipments",
                newName: "Carrier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7e93321f-7297-4abd-ab9b-d88158537dba", "AQAAAAIAAYagAAAAEFEpm1vFxd3wnF2+vFP+wEj/4Zg4+pdl207HiaUcKswj/8C9MsQG82DvjANQ72ZDJg==", "58c59f22-0e6d-4051-9b26-78d16ab2b917" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7eebac8d-8afc-4939-b100-0b583cb9473b", "AQAAAAIAAYagAAAAECOX9V9OyK0GyfUf5IgAkKD60IW7oTo2suYQwOcnRmtNiLxYxHM7K7D8VvyuT1ozew==", "feac7401-cd6f-4aa6-bc6f-1341f43e85c1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Carrier",
                table: "Shipments",
                newName: "Carrieer");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff022cf3-9771-4b50-b3ef-eaf5bdf879d6", "AQAAAAIAAYagAAAAEFjUFFHJ1yE1MPQb/u2p3D7AgJ1VN1uY5vHwhJjZZ/tr1/wamXMb8jJkVMd/yu6CQA==", "5d7d06de-0d19-4145-a8f2-d5fd898a588a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1c0f490b-7526-4dae-ab83-034d741e4bc4", "AQAAAAIAAYagAAAAEIrAjtTc8lInqqLhjH9IXH9zngjYNIw39vZB3sD5fA7WywqVQo2+gV+yICQm/eyXgQ==", "ff33086d-76a6-47fc-ac9e-42e47214be4c" });
        }
    }
}
