namespace ParkXUI.Models.PaymentModel;

public class KBankGatewayResultModel
{
    public string key { get; set; }
    public string orderId { get; set; }
    public string packageId { get; set; }
    public int maxCar { get; set; }
    public int coCurrent { get; set; }
    public int qty { get; set; }
    public int amount { get; set; }
}