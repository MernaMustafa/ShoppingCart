using Microsoft.EntityFrameworkCore.Migrations;

namespace ShoppingCart.Migrations
{
    public partial class AddAdmins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "UserId", "UserName", "Password",
                                "Email", "FirstName", "LastName", "Type"},
              values: new object[] { 1, "admin", "admin", "admin@gmail.com" ,
                                     "Merna", "Mustafa", "admin"});

            migrationBuilder.InsertData(
              table: "Users",
              columns: new[] { "UserId", "UserName", "Password",
                                "Email", "FirstName", "LastName", "Type"},
              values: new object[] { 2, "admin2", "admin2", "admin2@gmail.com" ,
                                     "Merna", "Ali", "admin"});

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
