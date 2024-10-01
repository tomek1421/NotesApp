using NotesApp.DTO;

namespace NotesApp.ServiceContracts;

public interface INotesService
{
    NoteResponse AddNote(Guid? subjectId, NoteAddRequest? noteAddRequest);
    
    NoteResponse? GetNoteById(Guid? subjectId, Guid? noteId);
    
    NoteResponse UpdateNoteContent(Guid? subjectId, Guid? noteId, NoteUpdateContentRequest? noteUpdateRequest);
    NoteResponse UpdateNoteTitle(Guid? subjectId, Guid? noteId, NoteUpdateTitleRequest? noteUpdateRequest);
    
    bool DeleteNote(Guid? subjectId, Guid? noteId);
}