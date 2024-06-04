namespace ParkXUI.Models.PaymentModel;

public class ChargePostResultApi
{
    public string id { get; set; }
    public string @object { get; set; }
    public double amount { get; set; }
    public string currency { get; set; }
    public string transaction_state { get; set; }
    public Source source { get; set; }
    public string created { get; set; }
    public string status { get; set; }
    public bool livemode { get; set; }
    public object failure_code { get; set; }
    public object failure_message { get; set; }
    public string description { get; set; }
    public Mpi mpi { get; set; }
    public string redirect_url { get; set; }
    public object settlement_info { get; set; }
    public object refund_info { get; set; }
    public object approval_code { get; set; }
    public object multi_clearing { get; set; }
    public string ref_1 { get; set; }
    public string ref_2 { get; set; }
    public object ref_3 { get; set; }
    public object dcc_data { get; set; }
    public string reference_order { get; set; }
    public object campaign_id { get; set; }
}

public class Source
{
    public string id { get; set; }
    public string @object { get; set; }
    public string brand { get; set; }
    public string card_masking { get; set; }
    public string issuer_bank { get; set; }
}

public class Mpi
{
    public object eci { get; set; }
    public object xid { get; set; }
    public object cavv { get; set; }
    public bool kbank_mpi { get; set; }
}