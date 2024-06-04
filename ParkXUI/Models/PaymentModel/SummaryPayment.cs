using ParkXUI.Models.Vehicles;

namespace ParkXUI.Models.PaymentModel;

public class SummaryPayment
{
    public string PackageName { get; set; }
    public string PackageType { get; set; }
    public string price { get; set; }
    public string startDate { get; set; }
    public string endDate { get; set; }
    public string maxCar { get; set; }
    
    public  string PaymentMethod { get; set; }
    public List<VehicleModel> vehicles { get; set; }
    public string coCurrent { get; set; }
    
}