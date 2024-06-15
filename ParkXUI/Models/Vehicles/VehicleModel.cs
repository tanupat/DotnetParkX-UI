using System.ComponentModel.DataAnnotations;

namespace ParkXUI.Models.Vehicles;

public class VehicleModel
{
    public string rowKey { get; set; }
    public string memberIdOrKey { get; set; }
    public string licensePlate { get; set; }
    public string province { get; set; }
    public string maker { get; set; }
    public string model { get; set; }
    public string year { get; set; }
    public string vehicleType { get; set; }
    public string attach { get; set; }
    public string filename { get; set; }
    public bool delete { get; set; }
}