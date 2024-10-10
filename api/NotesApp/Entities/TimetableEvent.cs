using System.ComponentModel.DataAnnotations;

namespace NotesApp.Entities;

public class TimetableEvent
{
    [Key]
    public Guid TimetableEventId { get; set; }
    
    [StringLength(50)]
    public string? EventName { get; set; }
    
    [StringLength(30)]
    public string? Teacher { get; set; }
    
    [StringLength(10)]
    public string? EventRoom { get; set; }
    
    [StringLength(10)]
    public string? Type { get; set; }
    
    [StringLength(10)]
    public string? Day { get; set; }
    
    [StringLength(10)]
    public string? StartTime { get; set; }
    
    [StringLength(10)]
    public string? EndTime { get; set; }
    
}