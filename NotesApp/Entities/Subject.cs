using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotesApp.Entities;

public class Subject
{
    [Key]
    public Guid SubjectId { get; set; }
    
    [StringLength(40)]
    public string? SubjectName { get; set; }
    
    [StringLength(300)]
    public string? SubjectDescription { get; set; }
    
    // navigation property
    public virtual ICollection<Note>? Notes { get; set; }
}