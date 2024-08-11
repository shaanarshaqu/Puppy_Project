using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Puppy_Project.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryTB",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ctg = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Profile_Photo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "user"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Active")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsTb_CategoryTB_Category_id",
                        column: x => x.Category_id,
                        principalTable: "CategoryTB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartTb_UsersTb_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTb_UsersTb_User_Id",
                        column: x => x.User_Id,
                        principalTable: "UsersTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListTb_UsersTb_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItemTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cart_id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Total = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItemTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItemTb_CartTb_Cart_id",
                        column: x => x.Cart_id,
                        principalTable: "CartTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItemTb_ProductsTb_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "ProductsTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    DelivaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemTb_OrderTb_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "OrderTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItemTb_ProductsTb_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "ProductsTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItemTb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishList_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItemTb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItemTb_ProductsTb_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "ProductsTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItemTb_WishListTb_WishList_Id",
                        column: x => x.WishList_Id,
                        principalTable: "WishListTb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItemTb_Cart_id",
                table: "CartItemTb",
                column: "Cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_CartItemTb_Product_Id",
                table: "CartItemTb",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_CartTb_UserId",
                table: "CartTb",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTb_Order_Id",
                table: "OrderItemTb",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemTb_Product_Id",
                table: "OrderItemTb",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTb_User_Id",
                table: "OrderTb",
                column: "User_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsTb_Category_id",
                table: "ProductsTb",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItemTb_Product_Id",
                table: "WishListItemTb",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItemTb_WishList_Id",
                table: "WishListItemTb",
                column: "WishList_Id");

            migrationBuilder.CreateIndex(
                name: "IX_WishListTb_UserId",
                table: "WishListTb",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItemTb");

            migrationBuilder.DropTable(
                name: "OrderItemTb");

            migrationBuilder.DropTable(
                name: "WishListItemTb");

            migrationBuilder.DropTable(
                name: "CartTb");

            migrationBuilder.DropTable(
                name: "OrderTb");

            migrationBuilder.DropTable(
                name: "ProductsTb");

            migrationBuilder.DropTable(
                name: "WishListTb");

            migrationBuilder.DropTable(
                name: "CategoryTB");

            migrationBuilder.DropTable(
                name: "UsersTb");
        }
    }
}
