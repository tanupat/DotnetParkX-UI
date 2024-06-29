using Microsoft.AspNetCore.Mvc.Rendering;
using ParkXUI.Models.Vehicles;

namespace ParkXUI.Interfaces;

public interface IVehicle
{
    Task<List<VehicleModel?>> GetVehiclesMember(string userId);
    
    Task<VehicleModel> GetVehicleDetail(string vehicleId);
    Task<bool> AddVehicle(VehicleModel vehicle);
    
    Task<bool> UpdateVehicle(VehicleModel vehicle);
    
    Task<bool> DeleteVehicle(VehicleModel vehicle);
    
    List<SelectListItem> GetVehicleType();
    
    Task<string> ConvertToBase64Async(IFormFile file);
    
    Task<bool> ActionVehicle(VehicleModel vehicle);
}