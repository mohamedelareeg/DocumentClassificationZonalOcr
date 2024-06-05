using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentClassificationZonalOcr.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_filePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ExportedMetaData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ExportedMetaData");
        }
    }
}
