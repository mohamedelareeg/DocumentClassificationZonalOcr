using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentClassificationZonalOcr.Api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormDetectionSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OcrEngine = table.Column<int>(type: "int", nullable: false),
                    FormSimilarity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DetectOptions = table.Column<int>(type: "int", nullable: false),
                    ZoneAllowedWidth = table.Column<double>(type: "float", nullable: false),
                    ZoneAllowedHeight = table.Column<double>(type: "float", nullable: false),
                    DetectAlgorithm = table.Column<int>(type: "int", nullable: false),
                    ActivePerspectiveTransform = table.Column<bool>(type: "bit", nullable: false),
                    ResizeImage = table.Column<bool>(type: "bit", nullable: false),
                    ConvertToGrayscale = table.Column<bool>(type: "bit", nullable: false),
                    Normalization = table.Column<bool>(type: "bit", nullable: false),
                    Blurring = table.Column<bool>(type: "bit", nullable: false),
                    EdgeDetection = table.Column<bool>(type: "bit", nullable: false),
                    HistogramEqualization = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDetectionSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: true),
                    FormId = table.Column<int>(type: "int", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Papers_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormSamples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormSamples", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormSamples_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    ActualWidth = table.Column<double>(type: "float", nullable: false),
                    ActualHeight = table.Column<double>(type: "float", nullable: false),
                    ActualImageWidth = table.Column<double>(type: "float", nullable: false),
                    ActualImageHeight = table.Column<double>(type: "float", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: true),
                    Regex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhiteList = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDuplicated = table.Column<bool>(type: "bit", nullable: false),
                    ZoneFieldType = table.Column<int>(type: "int", nullable: false),
                    FormSampleId = table.Column<int>(type: "int", nullable: false),
                    IsAnchorPoint = table.Column<bool>(type: "bit", nullable: false),
                    AnchorPointPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_FormSamples_FormSampleId",
                        column: x => x.FormSampleId,
                        principalTable: "FormSamples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FormId",
                table: "Fields",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormSamples_FormId",
                table: "FormSamples",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Papers_DocumentId",
                table: "Papers",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_FormSampleId",
                table: "Zones",
                column: "FormSampleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "FormDetectionSettings");

            migrationBuilder.DropTable(
                name: "Papers");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "FormSamples");

            migrationBuilder.DropTable(
                name: "Forms");
        }
    }
}
