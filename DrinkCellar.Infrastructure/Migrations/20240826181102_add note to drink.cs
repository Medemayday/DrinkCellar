﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrinkCellar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addnotetodrink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Drinks",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Drinks");
        }
    }
}