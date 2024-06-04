using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ParkXUI.Interfaces;
using ParkXUI.ViewModel.Components;

namespace ParkXUI.ViewComponents;

public class CardSummaryViewComponent:ViewComponent
{
    private readonly IAuth _authService;
    
    public CardSummaryViewComponent(IAuth authService)
    {
        _authService = authService;
    }
    
    public IViewComponentResult Invoke()
    {
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userModel =  _authService.GetUserDetail(userId).Result;
        CardSummaryViewModel model = new CardSummaryViewModel();
        model.vehicleCount = userModel.memberVehicle.Count;//userModel.data.;
        model.packagesCount = 1;
        return View(model);
    }
   
}