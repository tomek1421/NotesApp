using NotesApp.Entities;
using NotesApp.RepositoryContracts;

namespace NotesApp.Repositories;

public class TimetableEventRepository : ITimetableEventRepository
{
    private readonly ApplicationDbContext _db;
    
    public TimetableEventRepository(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public TimetableEvent AddEvent(TimetableEvent timetableEvent)
    {
        _db.TimetableEvents.Add(timetableEvent);
        _db.SaveChanges();
        
        return timetableEvent;
    }

    public List<TimetableEvent> GetAllEvents()
    {
        return _db.TimetableEvents.ToList();
    }

    public TimetableEvent? GetEventById(Guid timetableEventId)
    {
        return _db.TimetableEvents.FirstOrDefault(temp => temp.TimetableEventId == timetableEventId);
    }

    public TimetableEvent UpdateEvent(TimetableEvent timetableEvent)
    {
        TimetableEvent? matchingTimetableEvent = _db.TimetableEvents.FirstOrDefault(temp => temp.TimetableEventId == timetableEvent.TimetableEventId);

        if (matchingTimetableEvent == null)
            return timetableEvent;
        
        matchingTimetableEvent.EventName = timetableEvent.EventName;
        matchingTimetableEvent.Teacher = timetableEvent.Teacher;
        matchingTimetableEvent.EventRoom = timetableEvent.EventRoom;
        matchingTimetableEvent.Type = timetableEvent.Type;
        matchingTimetableEvent.Day = timetableEvent.Day;
        matchingTimetableEvent.StartTime = timetableEvent.StartTime;
        matchingTimetableEvent.EndTime = timetableEvent.EndTime;
        
        _db.SaveChanges();
        return matchingTimetableEvent;
    }

    public bool DeleteEvent(Guid timetableEventId)
    {
        _db.TimetableEvents.RemoveRange(_db.TimetableEvents.Where(temp => temp.TimetableEventId == timetableEventId));
        
        int rowsDeleted = _db.SaveChanges();
        return rowsDeleted > 0;
    }
}