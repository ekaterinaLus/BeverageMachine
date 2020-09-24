using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class UpdatePurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                column: "ShoppingBasketViewModelId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods");

            migrationBuilder.AlterColumn<int>(
                name: "ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "PurchasedGoods",
                column: "ShoppingBasketViewModelId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
