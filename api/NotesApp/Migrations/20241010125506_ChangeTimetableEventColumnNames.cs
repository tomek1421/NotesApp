using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTimetableEventColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimetableEvents",
                keyColumn: "TimetableEventId",
                keyValue: new Guid("a946f8db-0724-45b9-b685-296cd0fc6b8d"));

            migrationBuilder.RenameColumn(
                name: "LectureRoom",
                table: "TimetableEvents",
                newName: "EventRoom");

            migrationBuilder.RenameColumn(
                name: "LectureName",
                table: "TimetableEvents",
                newName: "EventName");

            migrationBuilder.InsertData(
                table: "TimetableEvents",
                columns: new[] { "TimetableEventId", "Day", "EndTime", "EventName", "EventRoom", "StartTime", "Teacher", "Type" },
                values: new object[] { new Guid("b8ad942e-90fb-4390-b3bd-bc2191b37e11"), "Monday", "11:30", "Default lecture", "1.14", "10:00", "dr Paweł Pączkowski", "lecture" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimetableEvents",
                keyColumn: "TimetableEventId",
                keyValue: new Guid("b8ad942e-90fb-4390-b3bd-bc2191b37e11"));

            migrationBuilder.RenameColumn(
                name: "EventRoom",
                table: "TimetableEvents",
                newName: "LectureRoom");

            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "TimetableEvents",
                newName: "LectureName");

            migrationBuilder.InsertData(
                table: "TimetableEvents",
                columns: new[] { "TimetableEventId", "Day", "EndTime", "LectureName", "LectureRoom", "StartTime", "Teacher", "Type" },
                values: new object[] { new Guid("a946f8db-0724-45b9-b685-296cd0fc6b8d"), "Monday", "11:30", "Default lecture", "1.14", "10:00", "dr Paweł Pączkowski", "lecture" });
        }
    }
}
