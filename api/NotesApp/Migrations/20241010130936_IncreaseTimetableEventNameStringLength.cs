using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class IncreaseTimetableEventNameStringLength : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimetableEvents",
                keyColumn: "TimetableEventId",
                keyValue: new Guid("b8ad942e-90fb-4390-b3bd-bc2191b37e11"));

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "TimetableEvents",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "TimetableEvents",
                columns: new[] { "TimetableEventId", "Day", "EndTime", "EventName", "EventRoom", "StartTime", "Teacher", "Type" },
                values: new object[] { new Guid("e10c30d8-2784-4df9-88bf-cf04b081c1a6"), "Monday", "11:30", "Default lecture", "1.14", "10:00", "dr Paweł Pączkowski", "lecture" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TimetableEvents",
                keyColumn: "TimetableEventId",
                keyValue: new Guid("e10c30d8-2784-4df9-88bf-cf04b081c1a6"));

            migrationBuilder.AlterColumn<string>(
                name: "EventName",
                table: "TimetableEvents",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "TimetableEvents",
                columns: new[] { "TimetableEventId", "Day", "EndTime", "EventName", "EventRoom", "StartTime", "Teacher", "Type" },
                values: new object[] { new Guid("b8ad942e-90fb-4390-b3bd-bc2191b37e11"), "Monday", "11:30", "Default lecture", "1.14", "10:00", "dr Paweł Pączkowski", "lecture" });
        }
    }
}
