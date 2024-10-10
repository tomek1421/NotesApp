using Microsoft.EntityFrameworkCore;

namespace NotesApp.Entities;

public class ApplicationDbContext : DbContext
{
    public virtual DbSet<Subject> Subjects { get; set; }
    public virtual DbSet<Note> Notes { get; set; }
    
    public virtual DbSet<TimetableEvent> TimetableEvents { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    
    //Migrations
    //Add migration -> dotnet ef migrations add <name>
    //Update database -> dotnet ef database update 
    //Remove migration -> dotnet ef migrations remove
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Subject>().ToTable("Subjects");
        modelBuilder.Entity<Note>().ToTable("Notes");
        modelBuilder.Entity<TimetableEvent>().ToTable("TimetableEvents");
        
        //Seed subjects data
        Subject subject = new Subject()
        {
            SubjectId = Guid.Parse("7FB0C231-415F-4DD7-8071-00B87C63A7C8"),
            SubjectName = "Default Subject",
            SubjectDescription = "Default Subject Description",
            DateOfCreation = "21/09/2024",
            Hashtags = "[\"daily\", \"math\", \"coding\"]"
        };
        
        modelBuilder.Entity<Subject>().HasData(subject);
        
        //Seed notes data
        Note note1 = new Note()
        {
            NoteId = Guid.Parse("CE118149-2642-47DC-BAFE-BD6A301F7B0D"),
            NoteTitle = "Default Note 1",
            NoteContent = "[{\"insert\":\"Title\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"First, solve the problem. Then, write the code. – John Johnson\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]",
            DateOfCreation = "21/09/2024",
            SubjectId = Guid.Parse("7FB0C231-415F-4DD7-8071-00B87C63A7C8")
        };

        Note note2 = new Note()
        {
            NoteId = Guid.Parse("D011BC43-16FF-476F-AFFC-CF5AF29AC9A2"),
            NoteTitle = "Default Note 2",
            NoteContent = "[{\"insert\":\"Title\"},{\"attributes\":{\"header\":1},\"insert\":\"\\n\"},{\"attributes\":{\"background\":\"#ffffff\",\"color\":\"#444444\"},\"insert\":\"It’s not a bug; it’s an undocumented feature. ― Anonymous\"},{\"attributes\":{\"blockquote\":true},\"insert\":\"\\n\"},{\"insert\":\"\\n\"}]",
            DateOfCreation = "21/09/2024",
            SubjectId = Guid.Parse("7FB0C231-415F-4DD7-8071-00B87C63A7C8")
        };
        
        modelBuilder.Entity<Note>().HasData(note1, note2);
        
        //seed timetable data
        TimetableEvent timetableEvent = new TimetableEvent()
        {
            TimetableEventId = Guid.NewGuid(),
            EventName = "Default lecture",
            Teacher = "dr Paweł Pączkowski",
            EventRoom = "1.14",
            Type = "lecture",
            Day = "Monday",
            StartTime = "10:00",
            EndTime = "11:30"
        };
        
        modelBuilder.Entity<TimetableEvent>().HasData(timetableEvent);
    }
}