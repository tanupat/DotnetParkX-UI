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
    public string siteLogo { get; set; }
    public string siteMapImage { get; set; }
    public string siteMapUrl { get; set; }
    public int WithholdingTax { get; set; }
    public string DateMonthFare { get; set; }
    public string DateHalfMonthFare { get; set; }
    public int IssueCardFee { get; set; }
    public int IssueCardMotorFee { get; set; }
    public int DepositFee { get; set; }
    public int DepositMotorFee { get; set; }
    public string Precis { get; set; }
    public string TypeCalculate { get; set; }
    public string SingleStampType { get; set; }
    public string MaxCardMemberCar { get; set; }
    public string MaxCardMemberMotor { get; set; }
    public string Branch { get; set; }
    public string ChangeDataFee { get; set; }
    public string ChangeDataMotorFee { get; set; }
    public string TurnstileAntipassback { get; set; }
    public string StampDefine { get; set; }
    public string BuildingName { get; set; }
    public string LprEnable { get; set; }
    public string ParkingAntipassback { get; set; }
    public string OnlineEnable { get; set; }
    public string AccessCctvFlag { get; set; }
    public string Country { get; set; }
    public string SiteNameReceipt { get; set; }
    public string SiteNameReport { get; set; }
    public string StampSymbol { get; set; }
    public string ReceiptType { get; set; }
    public int CarNumber { get; set; }
    public int MotorNumber { get; set; }
    public int CarSpace { get; set; }
    public int MotorSpace { get; set; }
    public string GPSLat { get; set; }
    public string GPSLong { get; set; }
    public string siteDescription { get; set; }
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
    public IEnumerable<LocalizedPlace> Places { get; set; }
}

public class LocalizedPlace
{
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