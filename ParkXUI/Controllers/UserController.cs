using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Auth;
using ParkXUI.Models.Vehicles;
using ParkXUI.Utility;
using ParkXUI.ViewModel.User;


namespace ParkXUI.Controllers;

public class UserController : Controller
{
    private readonly IAuth _authService;
    private readonly HttpClientUtility _httpClientUtility;
    private readonly IVehicle _vehicleService;
    public UserController(IAuth authService, HttpClientUtility httpClientUtility, IVehicle vehicleService)
    {
        _authService = authService;
        _httpClientUtility = httpClientUtility;
        _vehicleService = vehicleService;
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
    
    public async Task<IActionResult> MemberVehicle()
    {
        MemberVehicleViewModel memberVehicle = new MemberVehicleViewModel();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _authService.GetUserDetail(userId);
        memberVehicle.vehicles = await _vehicleService.GetVehiclesMember(user.memberKey);
        return View(memberVehicle);
    }
    
    public IActionResult AddVehicle()
    {
        AddVehicleViewModel addVehicle = new AddVehicleViewModel();
        addVehicle.vehicleType = _vehicleService.GetVehicleType();
        return View(addVehicle);
    }
    
    [HttpPost]
    public async Task<IActionResult> AddVehicle(AddVehicleViewModel addVehicle)
    {
        try
        {
            
            addVehicle.vehicleType = _vehicleService.GetVehicleType();
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _authService.GetUserDetail(userId);
            if (addVehicle.file != null)
            {
                addVehicle.vehicle.filename = addVehicle.file.FileName;
                addVehicle.vehicle.attach = await _vehicleService.ConvertToBase64Async(addVehicle.file);
            }
            addVehicle.vehicle.memberIdOrKey = user.memberKey;
            addVehicle.vehicle.delete = false;
            await _vehicleService.ActionVehicle(addVehicle.vehicle);
            return RedirectToAction("MemberVehicle");
        }
        catch (Exception e)
        {
            ViewBag.error = e.Message;
            return View(addVehicle);
        }
    }
    
    public async Task<IActionResult> EditVehicle(string vehicleId)
    {
        EditVehicleViewModel addVehicle = new EditVehicleViewModel();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var vehicle = await _vehicleService.GetVehiclesMember(userId);
        addVehicle.vehicle = vehicle.FirstOrDefault(x => x.rowKey == vehicleId);
        addVehicle.vehicleType = _vehicleService.GetVehicleType();
    
        

        return View(addVehicle);
    }
    
    [HttpPost]
    public async Task<IActionResult> EditVehicle(EditVehicleViewModel editVehicle)
    {
        try
        {
            editVehicle.vehicleType = _vehicleService.GetVehicleType();
            
                var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var vehicle = await _vehicleService.GetVehiclesMember(userId);
                // editVehicle.vehicle = editVehicle.vehicle; //vehicle.FirstOrDefault(x => x.rowKey == editVehicle.vehicle.rowKey);
                if (editVehicle.file != null)
                {
                    editVehicle.vehicle.filename = editVehicle.file.FileName;
                    editVehicle.vehicle.attach = await _vehicleService.ConvertToBase64Async(editVehicle.file);
                }
                 editVehicle.vehicle.delete = false;
                 editVehicle.vehicle.memberIdOrKey = userId;
                 editVehicle.vehicle.rowKey = editVehicle.vehicle.rowKey;
                await _vehicleService.ActionVehicle(editVehicle.vehicle);
                
            return RedirectToAction("MemberVehicle");
        }catch (Exception e)
        {
            ViewBag.error = e.Message;
            return View(editVehicle);
        }

   
    }
    
    public async Task<IActionResult> GetVehicleById(string memberId)
    {
        try
        {
            var result = await _vehicleService.GetVehiclesMember(memberId);
        
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}