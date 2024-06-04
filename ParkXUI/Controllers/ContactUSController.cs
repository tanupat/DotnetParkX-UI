using Microsoft.AspNetCore.Mvc;

namespace ParkXUI.Controllers;

public class ContactUSController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}