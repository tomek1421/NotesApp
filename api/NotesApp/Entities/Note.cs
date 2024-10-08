using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Entities;

public class Note
{
    [Key]
    public Guid NoteId { get; set; }
    
    [StringLength(40)]
    public string? NoteTitle { get; set; }
    
    [StringLength(int.MaxValue)]
    public string? NoteContent { get; set; }
    
    //foreign key property
    public Guid? SubjectId { get; set; }
    
    [StringLength(30)]
    public string? DateOfCreation { get; set; }
    
    //navigation property
    public virtual Subject? Subject { get; set; } // This makes SubjectId a foreign key
}