using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Clayton.Migrations
{
    public partial class Enhancement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enhancements",
                columns: table => new
                {
                    EnhancementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompletedDate = table.Column<DateTime>(nullable: true),
                    Content = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enhancements", x => x.EnhancementId);
                });

            migrationBuilder.CreateTable(
                name: "EnhancementProgress",
                columns: table => new
                {
                    EnhancementProgressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    EnhancementId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnhancementProgress", x => x.EnhancementProgressId);
                    table.ForeignKey(
                        name: "FK_EnhancementProgress_Enhancements_EnhancementId",
                        column: x => x.EnhancementId,
                        principalTable: "Enhancements",
                        principalColumn: "EnhancementId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_EnhancementProgress_EnhancementId",
                table: "EnhancementProgress",
                column: "EnhancementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnhancementProgress");

            migrationBuilder.DropTable(
                name: "Enhancements");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Post",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "Post",
                nullable: true);
        }
    }
}
