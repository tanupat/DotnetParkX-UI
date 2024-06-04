using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Models.Site;
using ParkXUI.Utility;
using ParkXUI.ViewModel.FindCarpark;

namespace ParkXUI.Controllers;

public class FindCarparkController : Controller
{
  
    // GET
    public async Task<IActionResult> Index()
    {
        var culture = CultureInfo.CurrentCulture.Name;
        string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "MockData", "sites.json");
        
        string JosnData = System.IO.File.ReadAllText(jsonPath);
        
        FindCarparkViewModel? site = JsonConvert.DeserializeObject<FindCarparkViewModel>(JosnData);
        
        site.sites = site.sites.Where(x => x.siteType == "parkX").ToList();
       
        
        
        return View(site);
    }
    

    public IActionResult ManagedByParkX()
    {
        return View();
    }
}