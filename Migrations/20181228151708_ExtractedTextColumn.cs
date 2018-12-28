using Microsoft.EntityFrameworkCore.Migrations;

namespace PDFRepositoryProject.Migrations
{
    public partial class ExtractedTextColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExtractedText",
                table: "PDFFileData",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExtractedText",
                table: "PDFFileData");
        }
    }
}
