using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GalleryApi.Migrations
{
    public partial class Another : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Kenneth_items",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Kenneth_items",
                newName: "FileName");

            migrationBuilder.AddColumn<string>(
                name: "FileDiscription",
                table: "Kenneth_items",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileExtension",
                table: "Kenneth_items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSizeInByte",
                table: "Kenneth_items",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDiscription",
                table: "Kenneth_items");

            migrationBuilder.DropColumn(
                name: "FileExtension",
                table: "Kenneth_items");

            migrationBuilder.DropColumn(
                name: "FileSizeInByte",
                table: "Kenneth_items");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Kenneth_items",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Kenneth_items",
                newName: "ImagePath");
        }
    }
}
