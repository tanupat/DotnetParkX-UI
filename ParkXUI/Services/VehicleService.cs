using System.Collections;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Vehicles;
using ParkXUI.Utility;

namespace ParkXUI.Services;

public class VehicleService:IVehicle
{
    private readonly HttpClientUtility _httpClientUtility;
    
    public VehicleService(HttpClientUtility httpClientUtility)
    {
        _httpClientUtility = httpClientUtility;
    }
    
    public async Task<List<VehicleModel?>> GetVehiclesMember(string userId)
    {
        try
        {
            List<VehicleModel?> vehicles = new List<VehicleModel?>();
            VehicleListModel vehicleListModel = new VehicleListModel();
            var response = await _httpClientUtility.GetAsync($"profiles/VehicleList?MemberIdOrKey={userId}");
             if (response.HttpStatus == HttpStatusCode.OK)
            {
                 vehicleListModel = JsonConvert.DeserializeObject<VehicleListModel>(response.Data);
                vehicles = vehicleListModel.vehicle;
            }
            else
            {
                vehicles = new List<VehicleModel?>();
                
            }

            return vehicles;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public Task<VehicleModel> GetVehicleDetail(string vehicleId)
    {
        try
        {
            VehicleModel vehicle = new VehicleModel();
            var response = _httpClientUtility.GetAsync($"profiles/VehicleDetail?VehicleId={vehicleId}");
            if (response.Result.HttpStatus == HttpStatusCode.OK)
            {
                vehicle = JsonConvert.DeserializeObject<VehicleModel>(response.Result.Data);
            }
            else
            {
                vehicle = new VehicleModel();
            }

            return Task.FromResult(vehicle);
        }catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public  async Task<bool> AddVehicle(VehicleModel vehicle)
    {
        try
        {
            var response = await _httpClientUtility.PostAsync("Profiles/Vehicle", vehicle);
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                return  true;
            }
            else
            {
                throw new Exception(response.MessageError);
            }
        }catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<bool> UpdateVehicle(VehicleModel vehicle)
    {
        try
        {
            var response = await _httpClientUtility.PutAsync("Profiles/Vehicle", vehicle);
            if(response.HttpStatus == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                throw new Exception(response.MessageError);
            }
            
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<bool> DeleteVehicle(VehicleModel vehicle)
    {
        try
        {
            var response = _httpClientUtility.PostAsync($"Profiles/Vehicle",vehicle);
            if (response.Result.HttpStatus == HttpStatusCode.OK)
            {
                return Task.FromResult(true);
            }
            else
            {
                throw new Exception(response.Result.MessageError);
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }


    }

    public IEnumerable<SelectListItem> GetVehicleType()
    {
        
        try
        {
            List<SelectListItem> vehicleType = new List<SelectListItem>();
            vehicleType.Add(new SelectListItem { Text = "รถยนตร์", Value = "car" });
            vehicleType.Add(new SelectListItem { Text = "รถจักยานยต์", Value = "Motorcycle" });
            
            return vehicleType;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<string> ConvertToBase64Async(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File is not valid");

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            return Convert.ToBase64String(fileBytes);
        }
    }

    public async Task<bool> ActionVehicle(VehicleModel vehicle)
    {
        try
        {
            var response = await _httpClientUtility.PostAsync("Profiles/Vehicle", vehicle);
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                return true;
            }else
            {
                throw new Exception(response.MessageError);
            }
        }catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}