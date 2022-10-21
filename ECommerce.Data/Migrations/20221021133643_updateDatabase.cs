using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce.Data.Migrations
{
    public partial class updateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderDetails_orders_orderDetailsId",
                table: "orderDetails");

            migrationBuilder.DropIndex(
                name: "IX_orderDetails_orderDetailsId",
                table: "orderDetails");

            migrationBuilder.DropColumn(
                name: "orderDetailsId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "orderDetailsId",
                table: "orderDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "orderDetailsId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "orderDetailsId",
                table: "orderDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_orderDetails_orderDetailsId",
                table: "orderDetails",
                column: "orderDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderDetails_orders_orderDetailsId",
                table: "orderDetails",
                column: "orderDetailsId",
                principalTable: "orders",
                principalColumn: "Id");
        }
    }
}
