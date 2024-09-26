using NotesApp.Entities;

namespace NotesApp.DTO;

public class SubjectWithNotesResponse
{
    public Guid SubjectId { get; set; }
    
    public string? SubjectName { get; set; }
    
    public string? SubjectDescription { get; set; }
    
    public string? DateOfCreation { get; set; }
    
    public string? Hashtags { get; set; }
    
    public List<NoteResponse>? Notes { get; set; }
}

static class SubjectExtension
{
    public static SubjectWithNotesResponse ToSubjectWithNotesResponse(this Subject subject, List<NoteResponse> notes)
    {
        return new SubjectWithNotesResponse()
        {
            SubjectId = subject.SubjectId,
            SubjectName = subject.SubjectName,
            SubjectDescription = subject.SubjectDescription,
            DateOfCreation = subject.DateOfCreation,
            Hashtags = subject.Hashtags,
            Notes = notes
        };
    }
}