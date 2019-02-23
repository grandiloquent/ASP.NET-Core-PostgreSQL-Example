using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApplication.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cover = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoDetail",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Height = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Duration = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoDetail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cover = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Tags = table.Column<List<string>>(nullable: true),
                    Thumbnail = table.Column<string>(nullable: true),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(maxLength: 512, nullable: false),
                    VoteDown = table.Column<int>(nullable: false),
                    VoteUp = table.Column<int>(nullable: false),
                    WatchedCount = table.Column<int>(nullable: false),
                    AlbumId = table.Column<long>(nullable: false),
                    VideoDetailId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Videos_VideoDetail_VideoDetailId",
                        column: x => x.VideoDetailId,
                        principalTable: "VideoDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Albums_Title",
                table: "Albums",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AlbumId",
                table: "Videos",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_VideoDetailId",
                table: "Videos",
                column: "VideoDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "VideoDetail");
        }
    }
}
