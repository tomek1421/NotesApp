using NotesApp.Entities;

namespace NotesApp.RepositoryContracts;

public interface ITimetableEventRepository
{ 
    TimetableEvent AddEvent(TimetableEvent timetableEvent);
    
    List<TimetableEvent> GetAllEvents();
    
    TimetableEvent? GetEventById(Guid timetableEventId);
    
    TimetableEvent UpdateEvent(TimetableEvent timetableEvent);
    
    bool DeleteEvent(Guid timetableEventId);
    
}