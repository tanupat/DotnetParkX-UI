using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkXUI.Models.Vehicles;

namespace ParkXUI.ViewModel.User;

public class EditVehicleViewModel
{
    public string memberKey { get; set; }
    public VehicleModel? vehicle { get; set; }
    
    public IFormFile file { get; set; }
    public List<SelectListItem>  vehicleType { get; set; }
    
    public bool delete { get; set; } 
    
}