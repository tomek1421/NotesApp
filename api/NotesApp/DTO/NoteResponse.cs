using NotesApp.Entities;

namespace NotesApp.DTO;

public class NoteResponse
{
    public Guid NoteId { get; set; }
    
    public string? NoteTitle { get; set; }
    
    public string? NoteContent { get; set; }
    
    public string? DateOfCreation { get; set; }
    
    public Guid? SubjectId { get; set; }
    
}

public static class NoteExtensions
{
    public static NoteResponse ToNoteResponse(this Note note)
    {
        NoteResponse noteResponse = new NoteResponse()
        {
            NoteId = note.NoteId,
            NoteTitle = note.NoteTitle,
            NoteContent = note.NoteContent,
            DateOfCreation = note.DateOfCreation,
            SubjectId = note.SubjectId
        };
        
        return noteResponse;
    }
}