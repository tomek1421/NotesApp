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

    public TimetableEventResponse? GetEventById(Guid? timetableEventId)
    {
        if (timetableEventId == null)
            throw new ArgumentNullException(nameof(timetableEventId));
        
        TimetableEvent? timetableEvent = _timetableEventRepository.GetEventById(timetableEventId.Value);

        if (timetableEvent == null)
            return null;
        
        return timetableEvent.ToTimetableEventResponse();
    }

    public TimetableEventResponse UpdateEvent(Guid? timetableEventId, TimetableEventUpdateRequest? timetableEventUpdateRequest)
    {
        if (timetableEventId == null || timetableEventUpdateRequest == null)
            throw new ArgumentNullException(nameof(timetableEventId));
        
        ValidationHelper.ModelValidation(timetableEventUpdateRequest);
        
        TimetableEvent? matchingTimetableEvent = _timetableEventRepository.GetEventById(timetableEventId.Value);

        if (matchingTimetableEvent == null)
            return null;
        
        matchingTimetableEvent.EventName = timetableEventUpdateRequest.EventName;
        matchingTimetableEvent.Teacher = timetableEventUpdateRequest.Teacher;
        matchingTimetableEvent.EventRoom = timetableEventUpdateRequest.EventRoom;
        matchingTimetableEvent.Type = timetableEventUpdateRequest.Type;
        matchingTimetableEvent.Day = timetableEventUpdateRequest.Day;
        matchingTimetableEvent.StartTime = timetableEventUpdateRequest.StartTime;
        matchingTimetableEvent.EndTime = timetableEventUpdateRequest.EndTime;
        
        _timetableEventRepository.UpdateEvent(matchingTimetableEvent);
        
        return matchingTimetableEvent.ToTimetableEventResponse();
    }

    public bool DeleteEvent(Guid? timetableEventId)
    {
        if (timetableEventId == null)
            throw new ArgumentNullException(nameof(timetableEventId));
        
        TimetableEvent? timetableEvent = _timetableEventRepository.GetEventById(timetableEventId.Value);

        if (timetableEvent == null)
            return false;
        
        _timetableEventRepository.DeleteEvent(timetableEventId.Value);

        return true;
    }
}