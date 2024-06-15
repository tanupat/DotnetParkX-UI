using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Site;
using ParkXUI.Utility;
using ParkXUI.ViewModel.FindCarpark;

namespace ParkXUI.Controllers;

public class FindCarparkController : Controller
{
    private readonly HttpClientUtility _httpClientUtility;
    private readonly ISites _sites;
    
    public FindCarparkController(HttpClientUtility httpClientUtility, ISites sites)
    {
        _httpClientUtility = httpClientUtility;
        _sites = sites;
    }
    
    
    // GET
    public async Task<IActionResult> Index()
    {
        FindCarparkViewModel site = new FindCarparkViewModel();
        var lang = CultureInfo.CurrentCulture.Name;
        try
        {
            site.sites = await _sites.GetSites(lang);
          
        }
        catch (Exception e)
        {
            ViewBag.error = e.Message;
        }
        
        //  string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "MockData", "sites.json");
        
       //  string JosnData = System.IO.File.ReadAllText(jsonPath);
        
       //indCarparkViewModel? site = JsonConvert.DeserializeObject<FindCarparkViewModel>(JosnData);
        
       //  site.sites = site.sites.Where(x => x.siteType == "parkX").ToList();
       
        
        
        return View(site);
    }
    

    public IActionResult ManagedByParkX()
    {
        return View();
    }
}