using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class Purchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods");

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "PurchasedGoods",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods");

            migrationBuilder.AlterColumn<int>(
                name: "DrinkId",
                table: "PurchasedGoods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PurchasedGoods_Drinks_DrinkId",
                table: "PurchasedGoods",
                column: "DrinkId",
                principalTable: "Drinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
