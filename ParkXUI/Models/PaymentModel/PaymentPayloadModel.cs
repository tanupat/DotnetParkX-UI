namespace ParkXUI.Models.PaymentModel;

public class PaymentPayloadModel
{
    public string amount { get; set; }
    public string currency { get; set; }
    public string paymentMethods { get;set; }
    public string name { get; set; }
    public string mid { get; set; }
    public string refNumber { get; set; }
    public string orderId { get; set; }
    public string ref_1 { get; set; }
    public string ref_2 { get; set; }
    public string srcScript { get; set; }
    public string apikey { get; set; }
}