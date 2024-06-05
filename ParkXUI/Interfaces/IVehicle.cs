using ParkXUI.Models.Vehicles;

namespace ParkXUI.Interfaces;

public interface IVehicle
{
    Task<List<VehicleModel>> GetVehiclesMember(string userId);
}