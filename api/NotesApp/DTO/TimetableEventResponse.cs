using NotesApp.Entities;

namespace NotesApp.DTO;

public class TimetableEventResponse
{
    public Guid TimetableEventId { get; set; }
    
    public string? EventName { get; set; }
    
    public string? Teacher { get; set; }

    public string? EventRoom { get; set; }
    
    public string? Type { get; set; }
    
    public string? Day { get; set; }
    
    public string? StartTime { get; set; }
    
    public string? EndTime { get; set; }
}

static class TimetableEventExtensions
{
    public static TimetableEventResponse ToTimetableEventResponse(this TimetableEvent timetableEvent)
    {
        return new TimetableEventResponse()
        {
            TimetableEventId = timetableEvent.TimetableEventId,
            EventName = timetableEvent.EventName,
            Teacher = timetableEvent.Teacher,
            EventRoom= timetableEvent.EventRoom,
            Type = timetableEvent.Type,
            Day = timetableEvent.Day,
            StartTime = timetableEvent.StartTime,
            EndTime = timetableEvent.EndTime
        };
    }
}