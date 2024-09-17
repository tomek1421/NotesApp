using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NotesApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    SubjectDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoteTitle = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId");
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "SubjectId", "SubjectDescription", "SubjectName" },
                values: new object[] { new Guid("7fb0c231-415f-4dd7-8071-00b87c63a7c8"), "Default Subject Description", "Default Subject" });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "NoteId", "NoteContent", "NoteTitle", "SubjectId" },
                values: new object[,]
                {
                    { new Guid("ce118149-2642-47dc-bafe-bd6a301f7b0d"), "[{\"insert\":\"Title\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"First, solve the problem. Then, write the code. – John Johnson\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]", "Default Note 1", new Guid("7fb0c231-415f-4dd7-8071-00b87c63a7c8") },
                    { new Guid("d011bc43-16ff-476f-affc-cf5af29ac9a2"), "[{\"insert\":\"Title\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"It’s not a bug; it’s an undocumented feature. ― Anonymous\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]", "Default Note 2", new Guid("7fb0c231-415f-4dd7-8071-00b87c63a7c8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_SubjectId",
                table: "Notes",
                column: "SubjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
