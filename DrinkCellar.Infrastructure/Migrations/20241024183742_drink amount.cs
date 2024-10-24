using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkCellar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class drinkamount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Drinks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Drinks");
        }
    }
}
