using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ParkXUI.Interfaces;
using ParkXUI.Models.Auth;
using ParkXUI.Utility;
using ParkXUI.ViewModel.User;

namespace ParkXUI.Controllers;

public class UserController : Controller
{
    private readonly IAuth _authService;
    
    public UserController(IAuth authService)
    {
        _authService = authService;
    }
    // GET
    [Authorize(Policy = PolicyLogin.Member)]
   public async Task<IActionResult> Profile()
   {
       ProfileModel profileData = new ProfileModel();
       var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
       var userModel = await _authService.GetUserDetail(userId);
       profileData.memberKey = userModel.memberKey;
       profileData.wrapUserId = userModel.data.wrapUserId;
       profileData.fullName = userModel.data.profiles.FirstOrDefault().fullName;
       profileData.email = userModel.data.profiles.FirstOrDefault().email;
       profileData.phone = userModel.data.profiles.FirstOrDefault().phone;
       profileData.address = userModel.data.profiles.FirstOrDefault().address;
       profileData.district = userModel.data.profiles.FirstOrDefault().district;
       profileData.subDistrict = userModel.data.profiles.FirstOrDefault().subDistrict;
       profileData.province = userModel.data.profiles.FirstOrDefault().province;
       profileData.zipCode = userModel.data.profiles.FirstOrDefault().zipCode;
       
         
       return View(profileData);
   }

   [HttpPost]
   [Authorize(Policy = PolicyLogin.Member)]
    public async Task<IActionResult> Profile(ProfileModel userModel)
    {
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _authService.GetUserDetail(userId);
      
        return View(userModel);
    }
    
}