using Microsoft.AspNetCore.Mvc;

namespace NotesApp.Controllers;

[Controller]
public class HomeController : Controller
{
    [Route("/")]
    public IActionResult Index()
    {
        return Content("Hello Notes App");
    }   
}