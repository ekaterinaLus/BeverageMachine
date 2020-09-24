using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class Goods2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "ShoppingBasket",
                newName: "ShoppingBaskets");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
