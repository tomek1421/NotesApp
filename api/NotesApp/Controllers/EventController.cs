using Microsoft.AspNetCore.Mvc;
using NotesApp.DTO;
using NotesApp.ServiceContracts;

namespace NotesApp.Controllers;

[Controller]
[Route("timetable")]
public class EventController : Controller
{
    private readonly ITimetableEventService _timetableEventService;
    
    public EventController(ITimetableEventService timetableEventService)
    {
        _timetableEventService = timetableEventService;
    }

    [HttpPost]
    public IActionResult CreateTimetableEvent([FromBody] TimetableEventAddRequest timetableEventAddRequest)
    {
        if (!ModelState.IsValid)
        {
            List<string> errorsList = new List<string>();
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    errorsList.Add(error.ErrorMessage);
                }
            }
            
            string errors = string.Join("\n", errorsList);
            return BadRequest(errors);
        }
        
        TimetableEventResponse timetableEventResponse = _timetableEventService.AddEvent(timetableEventAddRequest);
        
        return Json(timetableEventResponse);
    }
    
    [HttpGet]
    public IActionResult GetEvents()
    {
        List<TimetableEventResponse> timetableEventResponse = _timetableEventService.GetAllEvents();

        return Json(timetableEventResponse);
    }
}