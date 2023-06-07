using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Entity.Migrations
{
    /// <inheritdoc />
    public partial class deletedat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UserStore");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "State");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "State");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "State");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MailToken");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "City");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "City");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UserStore",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "User",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "State",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "State",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "State",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MailToken",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Country",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Country",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Country",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "City",
                type: "datetime",
                nullable: false,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "City",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "City",
                type: "datetime",
                nullable: true);
        }
    }
}
