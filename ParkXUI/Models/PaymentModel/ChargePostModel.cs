namespace ParkXUI.Models.PaymentModel;

public class ChargePostModel
{
  public int amount { get; set; }
  public string currency { get; set; }
  public string description { get; set; }
  public string source_type { get; set; }
  public string mode { get; set; }
  public string token   { get; set; }
  public string reference_order { get; set; }
  public string ref_1 { get; set; }
  public string ref_2 { get; set; }
  public AdditionalData additional_data { get; set; }
  public string callback_type { get; set; }
  
}

