using System.ComponentModel.DataAnnotations;

namespace NotesApp.DTO;

public class TimetableEventUpdateRequest
{
    [Required(ErrorMessage = "Lecture Name can't be empty")]
    [StringLength(50)]
    public string? EventName { get; set; }
    
    [Required(ErrorMessage = "Teacher can't be empty")]
    [StringLength(30)]
    public string? Teacher { get; set; }

    [Required(ErrorMessage = "Lecture Room can't be empty")]
    [RegularExpression(@"^[1-5]\.(0?[1-9]|[1-9]\d|100)$|^home$", ErrorMessage = "Event Room must be in the format X.Y, where X is between 1 and 5, and Y is between 1 and 100 or 'home'")]
    [StringLength(10)]
    public string? EventRoom { get; set; }
    
    [Required(ErrorMessage = "Type can't be empty")]
    [RegularExpression(@"^(practise|lecture|internship)$", ErrorMessage = "Type must be either 'practise' or 'lecture' or 'internship")]
    [StringLength(10)]
    public string? Type { get; set; }
    
    [Required(ErrorMessage = "Day Name can't be empty")]
    [RegularExpression(@"^(Monday|Tuesday|Wednesday|Thursday|Friday)$", ErrorMessage = "Day must be a valid weekday (Monday to Friday)")]
    [StringLength(10)]
    public string? Day { get; set; }
    
    [Required(ErrorMessage = "Start Time Name can't be empty")]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Start Time must be in the format HH:MM and between 00:00 and 23:59")]

    [StringLength(10)]
    public string? StartTime { get; set; }
    
    [Required(ErrorMessage = "End Time can't be empty")]
    [RegularExpression(@"^([01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "End Time must be in the format HH:MM and between 00:00 and 23:59")]
    [StringLength(10)]
    public string? EndTime { get; set; }
}