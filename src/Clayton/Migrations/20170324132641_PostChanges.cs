using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clayton.Migrations
{
    public partial class PostChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Post",
                nullable: false,
                defaultValue: false);

            migrationBuilder.RenameColumn(
                name: "Createdate",
                table: "Post",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Post");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Post",
                newName: "Createdate");
        }
    }
}
