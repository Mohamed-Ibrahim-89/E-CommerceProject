using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceProject.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ModifyModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a5def06b-62ff-4240-8e35-82e12a006d89", "AQAAAAIAAYagAAAAECuWHegv1Xbnzlrtrnllw+5h8cZl1bGXsgdcvglrwEJoZZH/rYtNsoSfLvecfZHD0Q==", "13d4e4fb-1b4b-439f-ba2e-897f16b475da" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "08bf8ab3-6e63-42d6-93ed-74c98c0da260", "AQAAAAIAAYagAAAAELMCOmTdJ09ZBerndKBnOYqaGuORNjP03hir8JxuQrBaXdlYfWkH+dpLuXrWxo1a6g==", "b9d6172e-b141-4899-8673-706c8b09194b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerInfoId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerInfoId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_Payments_CustomerInfo_CustomerInfoId",
                        column: x => x.CustomerInfoId,
                        principalTable: "CustomerInfo",
                        principalColumn: "CustomerInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "CustomerInfoId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8843c18-ef90-483f-905a-326da015a563", null, "AQAAAAIAAYagAAAAEMRoy/F7POGKRMyufLddv3SuSOK4hz+In6XjHpEowiA3dy4CifsQmgcW9Ey2YOQ46Q==", "c9ff6028-edd3-4e7b-9726-00c8951fc016" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "CustomerInfoId", "PasswordHash", "SecurityStamp" },
                values: new object[] { "37d56e12-0279-4893-b21c-105fc3e8be35", null, "AQAAAAIAAYagAAAAEFO7a6EmCyZYzpCFZX+N+nZWpklsIGOWFx7g0zBiRx7NIGPK5b63QPAcRX4Ffrp/0g==", "faf537cf-4ae7-48ff-b5e4-f93b8a400651" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerInfoId",
                table: "Payments",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CustomerInfo_CustomerInfoId",
                table: "AspNetUsers",
                column: "CustomerInfoId",
                principalTable: "CustomerInfo",
                principalColumn: "CustomerInfoId");
        }
    }
}
