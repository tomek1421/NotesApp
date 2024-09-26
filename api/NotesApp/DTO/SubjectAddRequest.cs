using System.ComponentModel.DataAnnotations;
using NotesApp.Entities;

namespace NotesApp.DTO;

public class SubjectAddRequest
{
    [Required(ErrorMessage = "Subject Name can't be empty")]
    public string? SubjectName { get; set; }
    
    [Required(ErrorMessage = "Subject Description can't be empty")]
    public string? SubjectDescription { get; set; }
    
    [Required(ErrorMessage = "Hashtags can't be empty")]
    public string? Hashtags { get; set; }

    public Subject ToSubject()
    {
        return new Subject()
        {
            SubjectName = SubjectName,
            SubjectDescription = SubjectDescription,
            Hashtags = Hashtags
        };
    }
}