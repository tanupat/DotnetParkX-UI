namespace ParkXUI.Models.PaymentModel;

public class OrderPaymentResult
{
    public string id { get; set; }
    public string @object { get; set; }
    public string created { get; set; }
    public bool livemode { get; set; }
    public double amount { get; set; }
    public string currency { get; set; }
    public object customer { get; set; }
    public string description { get; set; }
    public object metadata { get; set; }
    public string status { get; set; }
    public string reference_order { get; set; }
    public string source_type { get; set; }
    public AdditionalData additional_data { get; set; }
    public object failure_code { get; set; }
    public object failure_message { get; set; }
    public int expire_time_seconds { get; set; }
}

public class AdditionalData
{
    public object term { get; set; }
    public object mid { get; set; }
    public object tid { get; set; }
    public object smartpay_id { get; set; }
    public object campaign_id { get; set; }
}