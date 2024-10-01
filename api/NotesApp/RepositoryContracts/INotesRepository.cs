using NotesApp.Entities;

namespace NotesApp.RepositoryContracts;

public interface INotesRepository
{
    Note AddNote(Note note);
    
    Note? GetNoteById(Guid subjectId, Guid noteId);
    
    Note UpdateNoteContent(Note note);
    Note UpdateNoteTitle(Note note);
    
    bool DeleteNote(Guid subjectId, Guid noteId);
}