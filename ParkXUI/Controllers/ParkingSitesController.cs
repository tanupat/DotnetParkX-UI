using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Models.Package;
using ParkXUI.Utility;
using ParkXUI.ViewModel.FindCarpark;

namespace ParkXUI.Controllers;

public class ParkingSitesController : Controller
{
    private readonly HttpClientUtility _httpClientUtility;
    
    public ParkingSitesController(HttpClientUtility httpClientUtility)
    {
        _httpClientUtility = httpClientUtility;
    }
    // GET
    public async Task<IActionResult> Index(string siteId)
    {
      

        ParkingSitesViewModel siteView = new ParkingSitesViewModel();
        try
        {
            string jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "MockData", "sites.json");
        
            string josnData = await System.IO.File.ReadAllTextAsync(jsonPath);
        
            FindCarparkViewModel? siteList = JsonConvert.DeserializeObject<FindCarparkViewModel>(josnData);
        
            var site = siteList?.sites.FirstOrDefault(x => x.siteId == siteId);
            siteView.site = site;
         
            var apiResultPackages = await _httpClientUtility.GetAsync($"Packages/Packages?active=Y&siteId={siteId}");
            if(apiResultPackages.HttpStatus == HttpStatusCode.OK)
            {
             
                siteView.packages = JsonConvert.DeserializeObject<List<PackageModel>>(apiResultPackages.Data);
            }else{
                siteView.packages = new List<PackageModel>();
            }
        }
        catch (Exception e)
        {
           ViewBag.error = e.Message;
        }
     
         
        return View(siteView);
    }
}