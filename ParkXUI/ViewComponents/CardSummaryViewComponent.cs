using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Package;
using ParkXUI.Utility;
using ParkXUI.ViewModel.Components;

namespace ParkXUI.ViewComponents;

public class CardSummaryViewComponent:ViewComponent
{
    private readonly IAuth _authService;
    private readonly HttpClientUtility _httpClientUtility;
    public CardSummaryViewComponent(IAuth authService, HttpClientUtility httpClientUtility)
    {
        _authService = authService;
        _httpClientUtility = httpClientUtility;
    }
    
    
    public IViewComponentResult Invoke()
    {
        List<PackageMemberModel> packageUserList = new List<PackageMemberModel>();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userModel =  _authService.GetUserDetail(userId).Result;
       var  packageUser = _httpClientUtility.GetAsync("Packages/PackageMember?memberIdOrKey="+userId).Result;

       if (packageUser.HttpStatus == HttpStatusCode.OK)
       {
              packageUserList = JsonConvert.DeserializeObject<List<PackageMemberModel>>(packageUser.Data);
         
       }


       CardSummaryViewModel model = new CardSummaryViewModel();
        model.vehicleCount = userModel.memberVehicle.Count;//userModel.data.;
        model.packagesCount = packageUserList.Count;
        return View(model);
    }
   
}