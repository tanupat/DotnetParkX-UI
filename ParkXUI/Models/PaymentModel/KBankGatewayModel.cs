using ParkXUI.Models.Vehicles;

namespace ParkXUI.Models.PaymentModel;

public class KBankGatewayModel
{
    public string memberIdOrKey { get; set; }
    public string packageIdOrKey { get; set; }
    public int qty { get; set; }
    public int maxCar { get; set; }
    public int coCurrent { get; set; }
    public string startDate { get; set; }
    public List<VehicleModel> car { get; set; }
}