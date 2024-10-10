using Microsoft.AspNetCore.Mvc;
using NotesApp.DTO;
using NotesApp.Enums;
using NotesApp.ServiceContracts;

namespace NotesApp.Controllers;

[Controller]
[Route("subjects")]
public class HomeController : Controller
{
    private readonly ISubjectsService _subjectsService;
    private readonly INotesService _notesService;
    private readonly ILogger<HomeController> _logger;
    
    //costructor
    public HomeController(ISubjectsService subjectsService, INotesService notesService, ILogger<HomeController> logger)
    {
        _subjectsService = subjectsService;
        _notesService = notesService;
        _logger = logger;
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        return Content("Hello Notes App");
    }

    [HttpGet]
    public IActionResult GetSubjects([FromQuery] string searchBy = nameof(SubjectWithNotesCountResponse.SubjectName), [FromQuery] string searchString = "", [FromQuery] string sortBy = nameof(SubjectWithNotesCountResponse.SubjectName), [FromQuery] SortOrderOptions sortOrder = SortOrderOptions.ASC)
    {
        
        List<SubjectWithNotesCountResponse> subjects = _subjectsService.GetFilteredSubjects(searchBy, searchString);
        
        List<SubjectWithNotesCountResponse> sortedSubjects = _subjectsService.GetSortedSubjects(subjects, sortBy, sortOrder);
        
        return Json(sortedSubjects);
        
    }

    [HttpPost]
    public IActionResult CreateSubject([FromBody] SubjectAddRequest subjectAddRequest)
    {
        
        //model validation
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
        SubjectResponse subjectResponse = _subjectsService.AddSubject(subjectAddRequest);
        
        return Json(subjectResponse);
    }

    [HttpPut]
    [Route("{subjectId}")]
    public IActionResult UpdateSubject([FromRoute] Guid subjectId,  [FromBody] SubjectUpdateRequest subjectUpdateRequest)
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
        
        SubjectResponse subjectResponse = _subjectsService.UpdateSubject(subjectId, subjectUpdateRequest);
        
        return Json(subjectResponse);
    }

    [HttpDelete]
    [Route("{subjectId}")]
    public IActionResult DeleteSubject([FromRoute] Guid subjectId)
    {
        bool isDeleted = _subjectsService.DeleteSubject(subjectId);
        
        if (!isDeleted)
            return NotFound("No such subject");
        
        return NoContent();
    }

    [HttpGet]
    [Route("{subjectId}/notes")]
    public IActionResult GetNotes([FromRoute] Guid subjectId)
    {
        SubjectResponse? subject = _subjectsService.GetSubjectById(subjectId);
        
        if (subject == null)
            return NotFound("No such subject");
        
        SubjectWithNotesResponse? subjectNoteResponse = _subjectsService.GetNotesBySubjectId(subjectId);
        
        return Json(subjectNoteResponse);
    }

    //Notes - endpoints
    
    [HttpGet]
    [Route("{subjectId}/notes/{noteId}")]
    public IActionResult GetNote([FromRoute] Guid subjectId, [FromRoute] Guid noteId)
    {
        NoteResponse? noteResponse = _notesService.GetNoteById(subjectId, noteId);
        
        if (noteResponse == null)
            return NotFound("No such note");

        return Json(noteResponse);
    }

    [HttpPost]
    [Route("{subjectId}/notes")]
    public IActionResult CreateNote([FromRoute] Guid subjectId, [FromBody] NoteAddRequest noteAddRequest)
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
        
        //TODO
        // while entering not existing subject id, database throw error 500
        
        NoteResponse noteResponse = _notesService.AddNote(subjectId, noteAddRequest);

        return Json(noteResponse);
    }

    [HttpPut]
    [Route("{subjectId}/notes/{noteId}")]
    public IActionResult UpdateNoteContent([FromRoute] Guid subjectId, [FromRoute] Guid noteId, [FromBody] NoteUpdateContentRequest noteUpdateContentRequest)
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
        
        NoteResponse noteResponse = _notesService.UpdateNoteContent(subjectId, noteId, noteUpdateContentRequest);
        
        return Json(noteResponse);
    }
    
    [HttpPut]
    [Route("{subjectId}/notes/{noteId}/title")]
    public IActionResult UpdateNoteTitle([FromRoute] Guid subjectId, [FromRoute] Guid noteId, [FromBody] NoteUpdateTitleRequest noteUpdateRequest)
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
        
        NoteResponse noteResponse = _notesService.UpdateNoteTitle(subjectId, noteId, noteUpdateRequest);
        
        return Json(noteResponse);
    }

    [HttpDelete]
    [Route("{subjectId}/notes/{noteId}")]
    public IActionResult DeleteNote([FromRoute] Guid subjectId, [FromRoute] Guid noteId)
    {
        bool isDeleted = _notesService.DeleteNote(subjectId, noteId);
        
        if (!isDeleted)
            return NotFound("No such note");

        return NoContent();
    }
}