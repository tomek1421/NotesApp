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

    public TimetableEvent GetEventById(Guid timetableEventId)
    {
        throw new NotImplementedException();
    }

    public TimetableEvent UpdateEvent(TimetableEvent timetableEvent)
    {
        throw new NotImplementedException();
    }

    public bool DeleteEvent(Guid timetableEventId)
    {
        throw new NotImplementedException();
    }
}