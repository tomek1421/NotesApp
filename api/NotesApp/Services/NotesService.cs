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
        
        //get date of creation
        note.DateOfCreation = DateTime.Now.ToString("dd/MM/yyyy");
        
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

    public NoteResponse UpdateNoteContent(Guid? subjectId, Guid? noteId, NoteUpdateContentRequest? noteUpdateRequest)
    {
        if (subjectId == null || noteId == null || noteUpdateRequest == null)
            throw new ArgumentNullException();
        
        //validation
        ValidationHelper.ModelValidation(noteUpdateRequest);
        
        //get matching Note
        Note? matchingNote = _notesRepository.GetNoteById(subjectId.Value, noteId.Value);
        
        //Change to message instead exception
        //maybe return null
        
        if (matchingNote == null)
            throw new ArgumentException("Given note id does not exist");
        
        matchingNote.NoteContent = noteUpdateRequest.NoteContent;
        
        //TODO
        //return this as noteResponse
        _notesRepository.UpdateNoteContent(matchingNote);
        
        return matchingNote.ToNoteResponse();

    }

    public NoteResponse UpdateNoteTitle(Guid? subjectId, Guid? noteId, NoteUpdateTitleRequest? noteUpdateRequest)
    {
        if (subjectId == null || noteId == null || noteUpdateRequest == null)
            throw new ArgumentNullException();
        
        //validation
        ValidationHelper.ModelValidation(noteUpdateRequest);
        
        //get matching Note
        Note? matchingNote = _notesRepository.GetNoteById(subjectId.Value, noteId.Value);
        
        //Change to message instead exception
        //maybe return null
        
        if (matchingNote == null)
            throw new ArgumentException("Given note id does not exist");

        matchingNote.NoteTitle = noteUpdateRequest.NoteTitle;
        
        //TODO
        //return this as noteResponse
        _notesRepository.UpdateNoteTitle(matchingNote);
        
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