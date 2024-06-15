using Microsoft.AspNetCore.Mvc.Rendering;
using ParkXUI.Models.Vehicles;

namespace ParkXUI.ViewModel.User;

public class AddVehicleViewModel
{
    public IEnumerable<SelectListItem>  vehicleType { get; set; }
    public VehicleModel vehicle { get; set; }
    public IFormFile file { get; set; }
}