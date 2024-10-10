using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class TimetableEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimetableEvents",
                columns: table => new
                {
                    TimetableEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LectureName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    Teacher = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    LectureRoom = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Day = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EndTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimetableEvents", x => x.TimetableEventId);
                });

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: new Guid("ce118149-2642-47dc-bafe-bd6a301f7b0d"),
                column: "DateOfCreation",
                value: "21/09/2024");

            migrationBuilder.UpdateData(
                table: "Notes",
                keyColumn: "NoteId",
                keyValue: new Guid("d011bc43-16ff-476f-affc-cf5af29ac9a2"),
                column: "DateOfCreation",
                value: "21/09/2024");

            migrationBuilder.InsertData(
                table: "TimetableEvents",
                columns: new[] { "TimetableEventId", "Day", "EndTime", "LectureName", "LectureRoom", "StartTime", "Teacher", "Type" },
                values: new object[] { new Guid("a946f8db-0724-45b9-b685-296cd0fc6b8d"), "Monday", "11:30", "Default lecture", "1.14", "10:00", "dr Paweł Pączkowski", "lecture" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimetableEvents");

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
    }
}
