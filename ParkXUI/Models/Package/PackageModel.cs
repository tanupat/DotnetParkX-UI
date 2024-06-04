namespace ParkXUI.Models.Package;

public class PackageModel
{
    public int index { get; set; }
    public string PackageID { get; set; }
    public string PackageName { get; set; }
    public string Description { get; set; }
    public string VehicleType { get; set; }
    public string MemberType { get; set; }
    public string PackageMain { get; set; }
    public string PackageType { get; set; }
    public string EventID { get; set; }
    public string EventType { get; set; }
    public DateTime RegisterStartDate { get; set; }
    public DateTime RegisterEndDate { get; set; }
    public DateTime StartDay { get; set; }
    public DateTime Endday { get; set; }
    public int TimeSale { get; set; }
    public int HourSale { get; set; }
    public int DaySale { get; set; }
    public int MonthSale { get; set; }
    public int YearSale { get; set; }
    public int CoCurrent { get; set; }
    public int FreeMin { get; set; }
    public int TimeInterval { get; set; }
    public int MaxCar { get; set; }
    public int Price { get; set; }
    public int NightRate { get; set; }
    public int Deposite { get; set; }
    public int LatePaymentFee { get; set; }
    public int TimeSet { get; set; }
    public int active { get; set; }
    public int Status { get; set; }
    public int PricePerCar { get; set; }
    public int PricePerSite { get; set; }
    public int PricePerCarMax { get; set; }
    public int FixSite { get; set; }
    public List<Site> Site { get; set; }
    public string headerText { get; set; }
    public string subText { get; set; }
    public string contentText { get; set; }
    public List<object> packageMember { get; set; }
    public string key { get; set; }
    public string imageUrl { get; set; }
}

public class Site
{
    public int SiteID { get; set; }
    public string SiteName { get; set; }
}