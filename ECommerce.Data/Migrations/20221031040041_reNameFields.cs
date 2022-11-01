using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Data.Migrations
{
    public partial class reNameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartDetails_carts_cartId",
                table: "cartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_cartDetails_products_productId",
                table: "cartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_AspNetUsers_userId",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_orderId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_products_productId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_AspNetUsers_userId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "products",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "products",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "productType",
                table: "products",
                newName: "ProductType");

            migrationBuilder.RenameColumn(
                name: "productImg",
                table: "products",
                newName: "ProductImg");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "inventoryNumber",
                table: "products",
                newName: "InventoryNumber");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "orders",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "dateOfPurchase",
                table: "orders",
                newName: "DateOfPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_orders_userId",
                table: "orders",
                newName: "IX_orders_UserId");

            migrationBuilder.RenameColumn(
                name: "rating",
                table: "orderDetails",
                newName: "Rating");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "orderDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "orderId",
                table: "orderDetails",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "datePurchase",
                table: "orderDetails",
                newName: "DatePurchase");

            migrationBuilder.RenameColumn(
                name: "comment",
                table: "orderDetails",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_productId",
                table: "orderDetails",
                newName: "IX_orderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_orderId",
                table: "orderDetails",
                newName: "IX_orderDetails_OrderId");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "carts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "total",
                table: "carts",
                newName: "Total");

            migrationBuilder.RenameIndex(
                name: "IX_carts_userId",
                table: "carts",
                newName: "IX_carts_UserId");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "cartDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "cartId",
                table: "cartDetails",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_cartDetails_productId",
                table: "cartDetails",
                newName: "IX_cartDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_cartDetails_cartId",
                table: "cartDetails",
                newName: "IX_cartDetails_CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartDetails_carts_CartId",
                table: "cartDetails",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartDetails_products_ProductId",
                table: "cartDetails",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_AspNetUsers_UserId",
                table: "carts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_OrderId",
                table: "orderDetails",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_products_ProductId",
                table: "orderDetails",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_AspNetUsers_UserId",
                table: "orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cartDetails_carts_CartId",
                table: "cartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_cartDetails_products_ProductId",
                table: "cartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_carts_AspNetUsers_UserId",
                table: "carts");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_OrderId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_products_ProductId",
                table: "orderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_AspNetUsers_UserId",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "products",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "products",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "products",
                newName: "productType");

            migrationBuilder.RenameColumn(
                name: "ProductImg",
                table: "products",
                newName: "productImg");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "InventoryNumber",
                table: "products",
                newName: "inventoryNumber");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "orders",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "orders",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "DateOfPurchase",
                table: "orders",
                newName: "dateOfPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_orders_UserId",
                table: "orders",
                newName: "IX_orders_userId");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "orderDetails",
                newName: "rating");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "orderDetails",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "orderDetails",
                newName: "orderId");

            migrationBuilder.RenameColumn(
                name: "DatePurchase",
                table: "orderDetails",
                newName: "datePurchase");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "orderDetails",
                newName: "comment");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_ProductId",
                table: "orderDetails",
                newName: "IX_orderDetails_productId");

            migrationBuilder.RenameIndex(
                name: "IX_orderDetails_OrderId",
                table: "orderDetails",
                newName: "IX_orderDetails_orderId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "carts",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "carts",
                newName: "total");

            migrationBuilder.RenameIndex(
                name: "IX_carts_UserId",
                table: "carts",
                newName: "IX_carts_userId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "cartDetails",
                newName: "productId");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "cartDetails",
                newName: "cartId");

            migrationBuilder.RenameIndex(
                name: "IX_cartDetails_ProductId",
                table: "cartDetails",
                newName: "IX_cartDetails_productId");

            migrationBuilder.RenameIndex(
                name: "IX_cartDetails_CartId",
                table: "cartDetails",
                newName: "IX_cartDetails_cartId");

            migrationBuilder.AddForeignKey(
                name: "FK_cartDetails_carts_cartId",
                table: "cartDetails",
                column: "cartId",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cartDetails_products_productId",
                table: "cartDetails",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_carts_AspNetUsers_userId",
                table: "carts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_orderId",
                table: "orderDetails",
                column: "orderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_products_productId",
                table: "orderDetails",
                column: "productId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_AspNetUsers_userId",
                table: "orders",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
