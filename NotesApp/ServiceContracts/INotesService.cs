using NotesApp.DTO;

namespace NotesApp.ServiceContracts;

public interface INotesService
{
    NoteResponse AddNote(Guid? subjectId, NoteAddRequest? noteAddRequest);
    
    NoteResponse? GetNoteById(Guid? subjectId, Guid? noteId);
    
    NoteResponse UpdateNote(Guid? subjectId, Guid? noteId, NoteUpdateRequest? noteUpdateRequest);
    
    bool DeleteNote(Guid? subjectId, Guid? noteId);
}