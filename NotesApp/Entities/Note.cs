using System.ComponentModel.DataAnnotations;

namespace NotesApp.Entities;

public class Note
{
    [Key]
    public Guid NoteId { get; set; }
    
    [StringLength(40)]
    public string? NoteTitle { get; set; }
    
    [StringLength(int.MaxValue)]
    public string? NoteContent { get; set; }
    
    public Guid? SubjectId { get; set; }
}