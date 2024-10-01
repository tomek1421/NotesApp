using System.ComponentModel.DataAnnotations;

namespace NotesApp.DTO;

public class NoteUpdateTitleRequest
{
    [Required(ErrorMessage = "Note Title can't be empty")]
    [StringLength(40)]
    public string? NoteTitle { get; set; }
}