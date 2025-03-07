﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfQLSV.Migrations
{
    /// <inheritdoc />
    public partial class RenameGrantColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Credit",
                table: "Subjects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Credit",
                table: "Subjects");
        }
    }
}
