using NotesApp.DTO;
using NotesApp.Helpers;
using NotesApp.ServiceContracts;
using NotesApp.Entities;
using NotesApp.RepositoryContracts;

namespace NotesApp.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;
    private readonly ILogger<NotesService> _logger;

    public NotesService(INotesRepository notesRepository, ILogger<NotesService> logger)
    {
        _notesRepository = notesRepository;
        _logger = logger;
    }
    
    public NoteResponse AddNote(Guid? subjectId, NoteAddRequest? noteAddRequest)
    {
        if (subjectId == null || noteAddRequest == null)
            throw new ArgumentNullException();
        
        //model validation
        ValidationHelper.ModelValidation(noteAddRequest);
        
        //Convert NoteAddRequest to Note
        Note note = noteAddRequest.ToNote();
        
        //Generate NoteId
        note.NoteId = Guid.NewGuid();
        
        //reassign Note to Subject
        note.SubjectId = subjectId;
        
        //TODO
        //return this as noteResponse
        _notesRepository.AddNote(note);
        
        return note.ToNoteResponse();
        
    }

    public NoteResponse? GetNoteById(Guid? subjectId, Guid? noteId)
    {
        if (noteId == null || subjectId == null)
            return null;
        
        Note? note = _notesRepository.GetNoteById(subjectId.Value, noteId.Value);

        if (note == null)
            return null;
        
        return note.ToNoteResponse();

    }

    public NoteResponse UpdateNote(Guid? subjectId, Guid? noteId, NoteUpdateRequest? noteUpdateRequest)
    {
        if (subjectId == null || noteId == null || noteUpdateRequest == null)
            throw new ArgumentNullException();
        
        //validation
        ValidationHelper.ModelValidation(noteUpdateRequest);
        
        //get matching Note
        Note? matchingNote = _notesRepository.GetNoteById(subjectId.Value, noteId.Value);
        
        //Change to message instead exception
        
        if (matchingNote == null)
            throw new ArgumentException("Given note id does not exist");
        
        matchingNote.NoteTitle = noteUpdateRequest.NoteTitle;
        matchingNote.NoteContent = noteUpdateRequest.NoteContent;
        
        //TODO
        //return this as noteResponse
        _notesRepository.UpdateNote(matchingNote);
        
        return matchingNote.ToNoteResponse();

    }

    public bool DeleteNote(Guid? subjectId, Guid? noteId)
    {
        if (subjectId == null || noteId == null)
            throw new ArgumentNullException(nameof(noteId));
        
        Note? note = _notesRepository.GetNoteById(subjectId.Value, noteId.Value);

        if (note == null)
            return false;
        
        //TODO
        //do sth with this
        _notesRepository.DeleteNote(subjectId.Value, noteId.Value);

        return true;
    }
}