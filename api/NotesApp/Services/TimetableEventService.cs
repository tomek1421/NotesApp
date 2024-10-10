using NotesApp.DTO;
using NotesApp.RepositoryContracts;
using NotesApp.ServiceContracts;
using NotesApp.Entities;
using NotesApp.Helpers;

namespace NotesApp.Services;

public class TimetableEventService : ITimetableEventService
{
    private readonly ITimetableEventRepository _timetableEventRepository;

    public TimetableEventService(ITimetableEventRepository timetableEventRepository)
    {
        _timetableEventRepository = timetableEventRepository;
    }

    public TimetableEventResponse AddEvent(TimetableEventAddRequest? timetableEventAddRequest)
    {
        if (timetableEventAddRequest == null)
            throw new ArgumentNullException(nameof(timetableEventAddRequest));
        
        //model validation
        ValidationHelper.ModelValidation(timetableEventAddRequest);
        
        TimetableEvent timetableEvent = timetableEventAddRequest.ToTimetableEvent();
        
        timetableEvent.TimetableEventId = Guid.NewGuid();
        
        _timetableEventRepository.AddEvent(timetableEvent);
        
        return timetableEvent.ToTimetableEventResponse();
    }

    public List<TimetableEventResponse> GetAllEvents()
    {
        List<TimetableEvent> timetableEvent = _timetableEventRepository.GetAllEvents();
        
        return timetableEvent.Select(temp => temp.ToTimetableEventResponse()).ToList();
    }
}