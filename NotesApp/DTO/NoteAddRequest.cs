using System.ComponentModel.DataAnnotations;
using NotesApp.Entities;

namespace NotesApp.DTO;

public class NoteAddRequest
{
    [Required(ErrorMessage = "Note Title can't be empty")]
    [StringLength(40)]
    public string? NoteTitle { get; set; }
    
    [Required(ErrorMessage = "Note Content can't be empty")]
    [StringLength(int.MaxValue)]
    public string? NoteContent { get; set; }

    public Note ToNote()
    {
        return new Note()
        {
            NoteTitle = NoteTitle,
            NoteContent = NoteContent
        };
    }
}