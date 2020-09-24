using Microsoft.EntityFrameworkCore.Migrations;

namespace BeverageMachine.Migrations
{
    public partial class Goods : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ShoppingBasket",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Name = table.Column<string>(nullable: true),
            //        Amount = table.Column<decimal>(nullable: false),
            //        Quantity = table.Column<int>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ShoppingBasket", x => x.Id);
            //    });

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_ShoppingBasket",
            //    table: "ShoppingBasket");

            migrationBuilder.RenameTable(
                name: "ShoppingBasket",
                newName: "ShoppingBaskets");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_ShoppingBaskets",
            //    table: "ShoppingBaskets",
            //    column: "Id");

            //migrationBuilder.CreateTable(
            //    name: "PurchasedGoods",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        IdGood = table.Column<int>(nullable: false),
            //        AmountGood = table.Column<decimal>(nullable: false),
            //        CustomerAmount = table.Column<decimal>(nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_PurchasedGoods", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchasedGoods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingBaskets",
                table: "ShoppingBaskets");

            migrationBuilder.RenameTable(
                name: "ShoppingBaskets",
                newName: "ShoppingBasketViewModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingBasketViewModel",
                table: "ShoppingBasketViewModel",
                column: "Id");
        }
    }
}
