using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkCellar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class drinkexpiration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ExperiationDate",
                table: "Drinks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperiationDate",
                table: "Drinks");
        }
    }
}
