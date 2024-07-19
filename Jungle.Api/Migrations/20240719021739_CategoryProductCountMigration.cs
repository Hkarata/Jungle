using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jungle.Api.Migrations
{
    /// <inheritdoc />
    public partial class CategoryProductCountMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductsCount",
                schema: "Stock",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductsCount",
                schema: "Stock",
                table: "Categories");
        }
    }
}
