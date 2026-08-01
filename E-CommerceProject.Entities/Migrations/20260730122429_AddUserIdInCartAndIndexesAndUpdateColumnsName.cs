using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_CommerceProject.Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdInCartAndIndexesAndUpdateColumnsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfo_AspNetUsers_AppUserId",
                table: "CustomerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_AppUserId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "ShoppingCartId",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Wishlists",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "WishlistId",
                table: "Wishlists",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists",
                newName: "IX_Wishlists_UserId");

            migrationBuilder.RenameColumn(
                name: "ShipmentId",
                table: "Shipments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "OrderIDetails",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DiscountId",
                table: "Discounts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "CustomerInfo",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CustomerInfoId",
                table: "CustomerInfo",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerInfo_AppUserId",
                table: "CustomerInfo",
                newName: "IX_CustomerInfo_UserId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Carts",
                newName: "Id");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Wishlists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Shipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Shipments",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "OrderIDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "OrderIDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Discounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomerInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CustomerInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Carts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Carts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "FullName", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b3cdf7f-7883-4165-b49d-925e17cd084f", null, null, "AQAAAAIAAYagAAAAEGqSRV+1zG/JDfphZb7rFWf57Dr5YWhAh6DZyI/zhJ0nNWyVc1UwI+J1Ou/oiS670Q==", "e289a68e-586b-4901-8685-753605ff5565" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "FullName", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0ffb2cd-dc01-4d89-889a-e46dfa5b677e", null, null, "AQAAAAIAAYagAAAAEHiyZogqxrGN3gSK0D4m6K7u0gEv1vlBtdUOHh6JMdScaHnl7YdYDGBheN1YkGfYIw==", "e6941897-8ec1-486a-897e-58be07d40f0c" });

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_CreatedAt",
                table: "Wishlists",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_UpdatedAt",
                table: "Wishlists",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_CreatedAt",
                table: "Shipments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Shipments_UpdatedAt",
                table: "Shipments",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedAt",
                table: "Products",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Description",
                table: "Products",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Price",
                table: "Products",
                column: "Price");

            migrationBuilder.CreateIndex(
                name: "IX_Products_QuantityInStock",
                table: "Products",
                column: "QuantityInStock");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedAt",
                table: "Products",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreatedAt",
                table: "Orders",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UpdatedAt",
                table: "Orders",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIDetails_CreatedAt",
                table: "OrderIDetails",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_OrderIDetails_UpdatedAt",
                table: "OrderIDetails",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_CreatedAt",
                table: "Discounts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_Name",
                table: "Discounts",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_UpdatedAt",
                table: "Discounts",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_CreatedAt",
                table: "CustomerInfo",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_UpdatedAt",
                table: "CustomerInfo",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedAt",
                table: "Categories",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UpdatedAt",
                table: "Categories",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CreatedAt",
                table: "Carts",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UpdatedAt",
                table: "Carts",
                column: "UpdatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfo_AspNetUsers_UserId",
                table: "CustomerInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_AspNetUsers_UserId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerInfo_AspNetUsers_UserId",
                table: "CustomerInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_UserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_CreatedAt",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_UpdatedAt",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_CreatedAt",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Shipments_UpdatedAt",
                table: "Shipments");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreatedAt",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Description",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Name",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Price",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_QuantityInStock",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UpdatedAt",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UpdatedAt",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderIDetails_CreatedAt",
                table: "OrderIDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderIDetails_UpdatedAt",
                table: "OrderIDetails");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_CreatedAt",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_Name",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_Discounts_UpdatedAt",
                table: "Discounts");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInfo_CreatedAt",
                table: "CustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_CustomerInfo_UpdatedAt",
                table: "CustomerInfo");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedAt",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_UpdatedAt",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CreatedAt",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UpdatedAt",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Shipments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "OrderIDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "OrderIDetails");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Discounts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomerInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CustomerInfo");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Wishlists",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Wishlists",
                newName: "WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_Wishlists_UserId",
                table: "Wishlists",
                newName: "IX_Wishlists_AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Shipments",
                newName: "ShipmentId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderIDetails",
                newName: "OrderDetailId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Discounts",
                newName: "DiscountId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CustomerInfo",
                newName: "AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CustomerInfo",
                newName: "CustomerInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerInfo_UserId",
                table: "CustomerInfo",
                newName: "IX_CustomerInfo_AppUserId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ShoppingCartId",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4a111",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2383b972-4236-4d90-8fd1-583572833795", "AQAAAAIAAYagAAAAEGb4eMoAGAoC6wd5pkU2B5iK1iDUUJTT29MfM6loopZYE1z3hSx0oVv/hY3QOmqjSQ==", "89916b6a-3939-461e-887e-d71892f08dd3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62fe5285-fd68-4711-ae93-673787f4ac66",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1e8fa4a3-73e7-47c5-a536-2a63ffed505b", "AQAAAAIAAYagAAAAEKB0V/K1hN3Y0QhhhIvTVEMrBEQPpUs/Fk/VpxWRpXnCnfRekIOcAvB/a6uDw5r4dA==", "b1e49aa9-a142-4961-805f-521b7dee0385" });

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerInfo_AspNetUsers_AppUserId",
                table: "CustomerInfo",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Discounts_DiscountId",
                table: "Products",
                column: "DiscountId",
                principalTable: "Discounts",
                principalColumn: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_AppUserId",
                table: "Wishlists",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
