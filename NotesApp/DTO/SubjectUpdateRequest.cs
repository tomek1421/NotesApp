using System.ComponentModel.DataAnnotations;

namespace NotesApp.DTO;

public class SubjectUpdateRequest
{
    [Required]
    public Guid SubjectId { get; set; }
    
    [Required(ErrorMessage = "Subject Name can't be empty")]
    public string? SubjectName { get; set; }
    
    [Required(ErrorMessage = "Subject Description can't be empty")]
    public string? SubjectDescription { get; set; }
}