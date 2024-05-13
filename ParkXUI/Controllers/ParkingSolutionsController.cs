using Microsoft.AspNetCore.Mvc;

namespace ParkXUI.Controllers;

public class ParkingSolutionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AboutParkX()
    {
        return View();
    }
}