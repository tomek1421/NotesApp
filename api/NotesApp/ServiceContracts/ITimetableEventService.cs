using NotesApp.DTO;

namespace NotesApp.ServiceContracts;

public interface ITimetableEventService
{
    TimetableEventResponse AddEvent(TimetableEventAddRequest? timetableEventAddRequest);
    List<TimetableEventResponse> GetAllEvents();
    TimetableEventResponse? GetEventById(Guid? timetableEventId);
    TimetableEventResponse UpdateEvent(Guid? timetableEventId, TimetableEventUpdateRequest? timetableEventUpdateRequest);
    bool DeleteEvent(Guid? timetableEventId);
}