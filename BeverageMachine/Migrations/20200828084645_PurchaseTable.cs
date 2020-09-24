using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class PurchaseTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ShoppingBaskets");

            migrationBuilder.DropColumn(
                name: "AmountGood",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "CustomerAmount",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "IdGood",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.AddColumn<int>(
                name: "DrinkId",
                table: "PurchasedGoods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "PurchasedGoods",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedGoods_DrinkId",
                table: "PurchasedGoods",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchasedGoods_ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                column: "ShoppingBasketViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                column: "ShoppingBasketViewModelId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedGoods_DrinkId",
                table: "PurchasedGoods");

            migrationBuilder.DropIndex(
                name: "IX_PurchasedGoods_ShoppingBasketViewModelId",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "DrinkId",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "PurchasedGoods");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketViewModelId",
                table: "PurchasedGoods");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ShoppingBaskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountGood",
                table: "PurchasedGoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CustomerAmount",
                table: "PurchasedGoods",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "IdGood",
                table: "PurchasedGoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBasketViewModelId",
                table: "Drinks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drinks_ShoppingBasketViewModelId",
                table: "Drinks",
                column: "ShoppingBasketViewModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Drinks_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "Drinks",
                column: "ShoppingBasketViewModelId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
