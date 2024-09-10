using Microsoft.AspNetCore.Mvc;
using NotesApp.DTO;
using NotesApp.Entities;
using NotesApp.ServiceContracts;

namespace NotesApp.Controllers;

[Controller]
public class HomeController : Controller
{
    private readonly ISubjectsService _subjectsService;
    private readonly ILogger<HomeController> _logger;
    
    //costructor
    public HomeController(ISubjectsService subjectsService, ILogger<HomeController> logger)
    {
        _subjectsService = subjectsService;
        _logger = logger;
    }
    
    [Route("/")]
    public IActionResult Index()
    {
        return Content("Hello Notes App");
    }

    [HttpGet]
    [Route("/subjects")]
    public IActionResult GetSubjects()
    {
        List<SubjectResponse> subjects = _subjectsService.GetAllSubjects();
        return Json(subjects);
    }

    [HttpPost]
    [Route("/subjects")]
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
    [Route("/subjects")]
    public IActionResult UpdateSubject([FromBody] SubjectUpdateRequest subjectUpdateRequest)
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
        
        SubjectResponse subjectResponse = _subjectsService.UpdateSubject(subjectUpdateRequest);
        
        return Json(subjectResponse);
    }

    [HttpDelete]
    [Route("/subjects/{subjectId}")]
    public IActionResult DeleteSubject([FromRoute] Guid subjectId)
    {
        bool isDeleted = _subjectsService.DeleteSubject(subjectId);
        
        if (!isDeleted)
            return NotFound("No such subject");
        
        return NoContent();
    }
    
}