using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Migrations
{
    public partial class AddItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "Items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 1, "Item_1", 100 });

            migrationBuilder.InsertData(
              table: "Items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 2, "Item_2", 200 });

            migrationBuilder.InsertData(
              table: "Items",
              columns: new[] { "ItemId", "Name", "Price" },
              values: new object[] { 3, "Item_3", 300 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
