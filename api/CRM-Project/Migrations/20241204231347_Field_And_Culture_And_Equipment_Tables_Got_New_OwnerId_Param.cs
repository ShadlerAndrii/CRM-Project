using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Project.Migrations
{
    /// <inheritdoc />
    public partial class Field_And_Culture_And_Equipment_Tables_Got_New_OwnerId_Param : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Fields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Equipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Cultures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Equipments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Cultures");
        }
    }
}
