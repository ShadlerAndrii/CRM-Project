using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Project.Migrations
{
    /// <inheritdoc />
    public partial class Culture_Table_Drop_Name_And_Price_Column_Also_Type_Column_Rename_To_Name_And_Added_New_Column_HarvestDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Cultures");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Cultures");

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Cultures",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "HarvestDate",
                table: "Cultures",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HarvestDate",
                table: "Cultures");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Cultures",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Cultures",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Cultures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
