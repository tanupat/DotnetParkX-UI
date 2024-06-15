using System.Diagnostics;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ParkXUI.Interfaces;
using ParkXUI.Models;
using ParkXUI.ViewModel.Home;

namespace ParkXUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private  readonly ISites _sites;
    public HomeController(ILogger<HomeController> logger,ISites sites)
    {
        _logger = logger;
        _sites = sites;
    }
   

    public IActionResult Index()
    {
        HomeViewModel homeModel = new HomeViewModel(); 
        var lang = CultureInfo.CurrentCulture.Name;
        try
        {
            homeModel.sites = _sites.GetSites(lang).Result;
        }
        catch (Exception e)
        {
            ViewBag.error = e.Message;
        }
        
        return View(homeModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}