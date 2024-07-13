using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jungle.Api.Migrations
{
    /// <inheritdoc />
    public partial class SoftDeleteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Tenancy",
                table: "Tenants",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Tenancy",
                table: "Tenants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Stock",
                table: "Products",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Stock",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                schema: "Sales",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Sales",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "OrderItems",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Sales",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "Customers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Sales",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                schema: "Stock",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "Stock",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Tenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Tenancy",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Stock",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Sales",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Sales",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Sales",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Sales",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                schema: "Stock",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "Stock",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                schema: "Sales",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
