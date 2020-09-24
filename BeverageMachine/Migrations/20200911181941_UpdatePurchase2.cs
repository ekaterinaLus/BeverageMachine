using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class UpdatePurchase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShoppingBaskets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingBaskets");
        }
    }
}
