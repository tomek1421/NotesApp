namespace NotesApp.DTO;
using Entities;

public class SubjectWithNotesCountResponse
{
    public Guid SubjectId { get; set; }
    
    public string? SubjectName { get; set; }
    
    public string? SubjectDescription { get; set; }
    
    public string? DateOfCreation { get; set; }
    
    public string? Hashtags { get; set; }
    
    public int NotesCount { get; set; }
}

static class SubjectExtension2
{
    public static SubjectWithNotesCountResponse ToSubjectWithNotesCountResponse(this Subject subject, int notesCount)
    {
        return new SubjectWithNotesCountResponse()
        {
            SubjectId = subject.SubjectId,
            SubjectName = subject.SubjectName,
            SubjectDescription = subject.SubjectDescription,
            DateOfCreation = subject.DateOfCreation,
            Hashtags = subject.Hashtags,
            NotesCount = notesCount
        };
    }
}