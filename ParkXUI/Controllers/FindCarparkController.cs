using Microsoft.AspNetCore.Mvc;

namespace ParkXUI.Controllers;

public class FindCarparkController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ManagedByParkX()
    {
        return View();
    }
}