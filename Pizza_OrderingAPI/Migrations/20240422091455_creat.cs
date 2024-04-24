using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizza_OrderingAPI.Migrations
{
    public partial class creat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SizeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExtraItemsId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemId);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityCode);
                });

            migrationBuilder.CreateTable(
                name: "ExtraItems",
                columns: table => new
                {
                    ExtraItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtraItems", x => x.ExtraItemId);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Users_Cities_CityCode",
                        column: x => x.CityCode,
                        principalTable: "Cities",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductMenus",
                columns: table => new
                {
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PizzaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PizzaNum = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMenus", x => x.PizzaId);
                    table.ForeignKey(
                        name: "FK_ProductMenus_ItemCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    DesiredDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Users",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PizzaSizes",
                columns: table => new
                {
                    SizeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaSizes", x => x.SizeId);
                    table.ForeignKey(
                        name: "FK_PizzaSizes_ProductMenus_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "ProductMenus",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderConfirmations",
                columns: table => new
                {
                    ConfirmationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderConfirmations", x => x.ConfirmationId);
                    table.ForeignKey(
                        name: "FK_OrderConfirmations_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    OrderItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PizzaId = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.OrderItemId);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_ProductMenus_PizzaId",
                        column: x => x.PizzaId,
                        principalTable: "ProductMenus",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "UserId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "CityCode", "CityName" },
                values: new object[,]
                {
                    { "C1", "City 1" },
                    { "C2", "City 2" }
                });

            migrationBuilder.InsertData(
                table: "ExtraItems",
                columns: new[] { "ExtraItemId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Extra Cheese", 1.50m },
                    { 2, "Bacon", 2.00m }
                });

            migrationBuilder.InsertData(
                table: "ItemCategory",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Category 1" },
                    { 2, "Category 2" }
                });

            migrationBuilder.InsertData(
                table: "OrderStatuses",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 1, "Pending" },
                    { 2, "Confirmed" },
                    { 3, "Delivered" },
                    { 4, "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "PizzaId", "CategoryId", "Description", "ImageURL", "PizzaName", "PizzaNum" },
                values: new object[,]
                {
                    { 1, 1, "Tomato, cheese, basil", null, "Margherita", 1 },
                    { 2, 2, "Pepperoni, cheese, tomato sauce", null, "Pepperoni", 2 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "CustomerId", "Address", "CityCode", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "123 Main St", "C1", "john@example.com", "John Doe", "123456789" },
                    { 2, "456 Elm St", "C2", "jane@example.com", "Jane Smith", "987654321" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "DeliveryAddress", "DesiredDeliveryTime", "OrderDate", "StatusId", "TotalAmount" },
                values: new object[] { 1, 1, "123 Main St", new DateTime(2024, 4, 23, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6749), new DateTime(2024, 4, 22, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6626), 1, 29.97m });

            migrationBuilder.InsertData(
                table: "PizzaSizes",
                columns: new[] { "SizeId", "Name", "PizzaId", "Price" },
                values: new object[,]
                {
                    { 1, "Small", 1, 8.99m },
                    { 2, "Medium", 1, 10.99m },
                    { 3, "Large", 1, 12.99m },
                    { 4, "Small", 2, 9.99m },
                    { 5, "Medium", 2, 11.99m },
                    { 6, "Large", 2, 13.99m }
                });

            migrationBuilder.InsertData(
                table: "OrderConfirmations",
                columns: new[] { "ConfirmationId", "ConfirmationDate", "ConfirmationMessage", "OrderId" },
                values: new object[] { 1, new DateTime(2024, 4, 22, 11, 14, 54, 635, DateTimeKind.Local).AddTicks(6809), "Your order has been confirmed.", 1 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "PizzaId", "Quantity", "Size", "Subtotal" },
                values: new object[] { 1, 1, 1, 2, "Small", 17.98m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderId", "PizzaId", "Quantity", "Size", "Subtotal" },
                values: new object[] { 2, 1, 2, 1, "Medium", 11.99m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderConfirmations_OrderId",
                table: "OrderConfirmations",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_PizzaId",
                table: "OrderItems",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaSizes_PizzaId",
                table: "PizzaSizes",
                column: "PizzaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMenus_CategoryId",
                table: "ProductMenus",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityCode",
                table: "Users",
                column: "CityCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ExtraItems");

            migrationBuilder.DropTable(
                name: "OrderConfirmations");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "PizzaSizes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductMenus");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ItemCategory");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
