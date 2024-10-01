using System.ComponentModel.DataAnnotations;

namespace NotesApp.DTO;

public class NoteUpdateContentRequest
{
    /*[Required(ErrorMessage = "Note Title can't be empty")]
    [StringLength(40)]
    public string? NoteTitle { get; set; }*/
    
    [Required(ErrorMessage = "Note Content can't be empty")]
    [StringLength(int.MaxValue)]
    public string? NoteContent { get; set; }
    
}