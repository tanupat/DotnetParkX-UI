namespace ParkXUI.Models.Site;

public class LocalizedSite
{
    public int SiteID { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string Tel { get; set; }
    public string Fax { get; set; }
    public int Vat { get; set; }
    public string TaxFlg { get; set; }
    public string TaxType { get; set; }
    public int WithholdingTax { get; set; }
    public int DateMonthFare { get; set; }
    public int DateHalfMonthFare { get; set; }
    public int IssueCardFee { get; set; }
    public int IssueCardMotorFee { get; set; }
    public int DepositFee { get; set; }
    public int DepositMotorFee { get; set; }
    public string Precis { get; set; }
    public int TypeCalculate { get; set; }
    public int SingleStampType { get; set; }
    public int MaxCardMemberCar { get; set; }
    public int MaxCardMemberMotor { get; set; }
    public string Branch { get; set; }
    public int ChangeDataFee { get; set; }
    public int ChangeDataMotorFee { get; set; }
    public int TurnstileAntipassback { get; set; }
    public int StampDefine { get; set; }
    public string BuildingName { get; set; }
    public int LprEnable { get; set; }
    public int ParkingAntipassback { get; set; }
    public int OnlineEnable { get; set; }
    public int AccessCctvFlag { get; set; }
    public string Country { get; set; }
    public int SiteNameReceipt { get; set; }
    public int SiteNameReport { get; set; }
    public string StampSymbol { get; set; }
    public int ReceiptType { get; set; }
    public int CarNumber { get; set; }
    public int MotorNumber { get; set; }
    public int CarSpace { get; set; }
    public int MotorSpace { get; set; }
    public double GPSLat { get; set; }
    public double GPSLong { get; set; }
    public IEnumerable<string> SiteRegulations { get; set; }
    public IEnumerable<LocalizedSiteRate> SiteRates { get; set; }
    public IEnumerable<LocalizedSiteNearby> SiteNearby { get; set; }
    public IEnumerable<LocalizedSiteContact> SiteContact { get; set; }
    public IEnumerable<LocalizedSiteImage> SiteImages { get; set; }
}

public class LocalizedSiteRate
{
    public string VehicleType { get; set; }
    public string HourlyRate { get; set; }
    public string DailyRate { get; set; }
    public string MonthlyRate { get; set; }
}

public class LocalizedSiteNearby
{
    public int PlaceGroup { get; set; }
    public string PlaceType { get; set; }
    public string PlaceIcon { get; set; }
    public string PlaceName { get; set; }
}

public class LocalizedSiteContact
{
    public string ContactName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
}

public class LocalizedSiteImage
{
    public string Image { get; set; }
    public string Description { get; set; }
}
