using NotesApp.DTO;

namespace NotesApp.ServiceContracts;

public interface ITimetableEventService
{
    TimetableEventResponse AddEvent(TimetableEventAddRequest? timetableEventAddRequest);
    List<TimetableEventResponse> GetAllEvents();
}