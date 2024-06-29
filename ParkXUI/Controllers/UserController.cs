using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Auth;
using ParkXUI.Models.User;
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
       ProfileViewModel profile = new ProfileViewModel();
       FormProfileModel profileData = new FormProfileModel();
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
       
       profile.profileData = profileData;
       return View(profile);
   }

   [HttpPost]
   [Authorize(Policy = PolicyLogin.Member)]
    public async Task<IActionResult> Profile(FormProfileModel profileData )
    {  ProfileViewModel profile = new ProfileViewModel();
        try
        {
          
            var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _authService.GetUserDetail(userId);
            var updateProfile = new UserDetailModel
            {
                key = user.memberKey,
                fullName = profileData.fullName,
                address = profileData.address,
                subDistrict = profileData.subDistrict,
                district = profileData.district,
                province = profileData.province,
                zipCode = profileData.zipCode,
                phone = profileData.phone,
                email = profileData.email,
                update = true,
                addresses = new List<Address>
                {
                    new Address
                    {
                        typeId = 1,
                        address = profileData.address,
                        subDistrict = profileData.subDistrict,
                        district = profileData.district,
                        province = profileData.province,
                        zipCode = profileData.zipCode,
                        phone = profileData.phone
                    }
                }
                
            };
            profile.profileData = profileData;
            
            var response = await _authService.UpdateProfile(updateProfile);

            if (response)
            {
                ViewBag.Success = "Update Profile Success";
            }
            else
            {
                ViewBag.Error = "Update Profile Fail";
            }

        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
        }
        
        return View(profile);
    }
    
    [HttpGet]
    [Authorize(Policy = PolicyLogin.Member)]
    public async Task<IActionResult> ConfirmOtp()
    { 
        ConfirmOtpViewModel confirmOtp = new ConfirmOtpViewModel();
        try
        {
            var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _authService.GetUserDetail(userId);
            var generator = new RandomGenerator();
            int randomNumber = generator.RandomNumber(100000, 999999);
            var request = new
            {
                MemberKey = user.memberKey,
                phone= user.data.profiles.FirstOrDefault().phone,
                password = randomNumber
            };
            
            var response = await _httpClientUtility.PostAsync("/auth/SendOTP", request);
            
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                confirmOtp.phoneNumber = user.data.profiles.FirstOrDefault().phone;
            }
            else
            {
                ViewBag.Error = response.MessageError;
            }
        }
        catch (Exception e)
        {
            ViewBag.Error = e.Message;
        }
        return View(confirmOtp);
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmOtp(ConfirmOtpViewModel confirmOtp)
    {
        try
        {
            var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _authService.GetUserDetail(userId);
            confirmOtp.phoneNumber = user.data.profiles.FirstOrDefault().phone;
            
            string Otp= confirmOtp.otp1 + confirmOtp.otp2 + confirmOtp.otp3 + confirmOtp.otp4 + confirmOtp.otp5 + confirmOtp.otp6;
            
            var request = new
            {
                MemberKey = user.memberKey,
                password = Otp
            };
            var response = _httpClientUtility.PostAsync("/auth/ConfirmedOTP", request).Result;
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                ResultConfirmOtpModel result = JsonConvert.DeserializeObject<ResultConfirmOtpModel>(response.Data);
                if (result.status)
                {
                    return RedirectToAction("Profile");
                }
                else
                {
                    ViewBag.Error = "OTP ไม่ถูกต้อง";
                }
            }
            else
            {
                ViewBag.Error = response.MessageError;
            }
        }catch (Exception e)
        {
            ViewBag.Error = e.Message;
        }

        return View(confirmOtp);
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
            addVehicle.vehicle.memberKeyOrId = user.memberKey;
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
        addVehicle.memberKey = userId;
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
                 
                 editVehicle.vehicle.memberKeyOrId = userId;
                 editVehicle.vehicle.rowKey = editVehicle.vehicle.rowKey;
                 editVehicle.vehicle.delete = editVehicle.delete;
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