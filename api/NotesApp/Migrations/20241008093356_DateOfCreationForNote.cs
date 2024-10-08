using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class DateOfCreationForNote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DateOfCreation",
                table: "Notes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: new Guid("ce118149-2642-47dc-bafe-bd6a301f7b0d"),
                column: "DateOfCreation",
                value: null);

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: new Guid("d011bc43-16ff-476f-affc-cf5af29ac9a2"),
                column: "DateOfCreation",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Notes");
        }
    }
}
