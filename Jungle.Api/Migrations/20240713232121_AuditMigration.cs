using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jungle.Api.Migrations
{
    /// <inheritdoc />
    public partial class AuditMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "Stock",
                table: "Products");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Tenancy",
                table: "Tenants",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Tenancy",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Stock",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Stock",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "OrderItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Sales",
                table: "Customers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Sales",
                table: "Customers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "Customers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Stock",
                table: "Categories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                schema: "Stock",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                schema: "Stock",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryProduct",
                schema: "Stock",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProduct", x => new { x.CategoriesId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalSchema: "Stock",
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalSchema: "Stock",
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProduct_ProductsId",
                schema: "Stock",
                table: "CategoryProduct",
                column: "ProductsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryProduct",
                schema: "Stock");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Tenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Tenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Sales",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Sales",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                schema: "Stock",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                schema: "Stock",
                table: "Categories");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "Stock",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Sales",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Sales",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Stock",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "Stock",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "Stock",
                table: "Products",
                column: "CategoryId",
                principalSchema: "Stock",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
