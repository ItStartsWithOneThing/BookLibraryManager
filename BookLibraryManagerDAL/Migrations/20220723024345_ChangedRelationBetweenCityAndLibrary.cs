using Microsoft.EntityFrameworkCore.Migrations;

namespace BookLibraryManagerDAL.Migrations
{
    public partial class ChangedRelationBetweenCityAndLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Libraries_CityId",
                table: "Libraries");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_CityId",
                table: "Libraries",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Libraries_CityId",
                table: "Libraries");

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_CityId",
                table: "Libraries",
                column: "CityId",
                unique: true);
        }
    }
}
