using System.Net;
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
    
    public async Task<List<VehicleModel>> GetVehiclesMember(string userId)
    {
        try
        {
            List<VehicleModel> vehicles = new List<VehicleModel>();
            VehicleListModel vehicleListModel = new VehicleListModel();
            var response = await _httpClientUtility.GetAsync($"profiles/VehicleList?MemberIdOrKey={userId}");
             if (response.HttpStatus == HttpStatusCode.OK)
            {
                 vehicleListModel = JsonConvert.DeserializeObject<VehicleListModel>(response.Data);
                vehicles = vehicleListModel.vehicle;
            }
            else
            {
                vehicles = new List<VehicleModel>();
                
            }

            return vehicles;

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }
}