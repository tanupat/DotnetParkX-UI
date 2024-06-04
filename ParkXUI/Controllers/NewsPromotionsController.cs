using Microsoft.AspNetCore.Mvc;

namespace ParkXUI.Controllers;

public class NewsPromotionsController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Promotions()
    {
        return View();
    }
    
}