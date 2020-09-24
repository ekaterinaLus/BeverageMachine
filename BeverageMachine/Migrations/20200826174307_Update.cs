using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ShoppingBaskets");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingBaskets");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBasketViewModelId",
                table: "Drinks",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drinks_ShoppingBaskets_ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.DropIndex(
                name: "IX_Drinks_ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketViewModelId",
                table: "Drinks");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "ShoppingBaskets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingBaskets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
