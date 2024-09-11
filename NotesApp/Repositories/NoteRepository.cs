using Microsoft.EntityFrameworkCore;
using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.RepositoryContracts;

namespace NotesApp.Repositories;

public class NoteRepository : INotesRepository
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<NoteRepository> _logger;
    
    public NoteRepository(ApplicationDbContext db, ILogger<NoteRepository> logger)
    {
        _db = db;
        _logger = logger;
    }
    
    public Note AddNote(Note note)
    {
        _db.Notes.Add(note);
        
        _db.SaveChanges();
        return note;
    }

    public Note? GetNoteById(Guid subjectId, Guid noteId)
    {
        return _db.Notes.FirstOrDefault(temp => temp.SubjectId == subjectId && temp.NoteId == noteId);
    }

    public Note UpdateNote(Note note)
    {
        Note? matchingNote = _db.Notes.FirstOrDefault(temp => temp.SubjectId == note.SubjectId && temp.NoteId == note.NoteId);

        if (matchingNote == null)
            return note;
        
        matchingNote.NoteTitle = note.NoteTitle;
        matchingNote.NoteContent = note.NoteContent;
        
        _db.SaveChanges();
        return matchingNote;
    }

    public bool DeleteNote(Guid subjectId, Guid noteId)
    {
        _db.Notes.RemoveRange(_db.Notes.Where(temp => temp.SubjectId == subjectId && temp.NoteId == noteId));
        
        int rowDeleted = _db.SaveChanges();
        return rowDeleted > 0;
    }
}