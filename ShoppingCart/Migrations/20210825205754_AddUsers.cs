using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Migrations
{
    public partial class AddUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] {"UserId", "UserName", "Password"},
              values: new object[] { 1, "Merna", "0123"});

            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "UserId", "UserName", "Password" },
              values: new object[] { 2, "Nada", "3456" });

            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "UserId", "UserName", "Password" },
              values: new object[] { 3, "Reham", "5678" });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
