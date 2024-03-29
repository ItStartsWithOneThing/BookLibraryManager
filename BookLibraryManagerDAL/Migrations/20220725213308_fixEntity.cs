﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibraryManagerDAL.Migrations
{
    public partial class fixEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Libraries",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Libraries");
        }
    }
}
