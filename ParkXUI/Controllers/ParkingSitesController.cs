using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Package;
using ParkXUI.Utility;
using ParkXUI.ViewModel.FindCarpark;
using ParkXUI.ViewModel.ParkingSites;

namespace ParkXUI.Controllers;

public class ParkingSitesController : Controller
{
    private readonly HttpClientUtility _httpClientUtility;
    private readonly ISites _sites;
    
    public ParkingSitesController(HttpClientUtility httpClientUtility,ISites sites)
    {
        _httpClientUtility = httpClientUtility;
        _sites = sites;
    }
    // GET
    public async Task<IActionResult> Index(string siteId)
    {
      

        ParkingSitesViewModel siteView = new ParkingSitesViewModel();
        try
        {
           
           var lang = CultureInfo.CurrentCulture.Name;
           try
           {
               var site = await _sites.GetSites(lang, siteId);
               siteView.sites = site.FirstOrDefault();

           }
           catch (Exception e)
           {
               ViewBag.error = e.Message;
           }
         
            var apiResultPackages = await _httpClientUtility.GetAsync($"Packages/Packages?active=Y&siteId={siteId}");
          // var apiResultPackages = await _httpClientUtility.GetAsync($"Packages/Packages?siteId={siteId}");
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
    
    [HttpPost]
    public  async Task<IActionResult> ComparePackages(ComparePackagesViewModel model)
    {
        if (model.PackageIds != null && model.PackageIds.Count > 0)
        {
            // Process the comparison of the packages
            // You can pass the model to a view, service, etc.
            List<PackageModel> packages = new List<PackageModel>();
            foreach (var packageId in model.PackageIds)
            {
                var apiPackageResult = await _httpClientUtility.GetAsync("Packages/Packages?packageIdOrKey=" + packageId);
        
                if (apiPackageResult.HttpStatus == HttpStatusCode.OK)
                {
                    var package = JsonConvert.DeserializeObject<List<PackageModel>>(apiPackageResult.Data);
                    packages.Add(package.FirstOrDefault());
                    //    packageDetailViewModel.Package = package;
                }
            }
            
            model.packages = packages;
            return View(model); // Assuming you have a view to display the comparison
        }
        return RedirectToAction("Index", "FindCarpark");
    }
}