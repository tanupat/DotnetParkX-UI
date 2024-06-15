namespace ParkXUI.Models.Site;


public class SiteList
{
    public List<SiteModel> Site { get; set; }
}

public class SiteModel
    {
        public int index { get; set; }
        public int SiteID { get; set; }
        public string CompanyNameLocale { get; set; }
        public string CompanyNameEng { get; set; }
        public string TaxID { get; set; }
        public string Addr1locale { get; set; }
        public string Addr1eng { get; set; }
        public string ZipCode { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public int Vat { get; set; }
        public string TaxFlg { get; set; }
        public string TaxType { get; set; }
        public int withholdingtax { get; set; }
        public string DateMonthFare { get; set; }
        public string DateHalfMonthFare { get; set; }
        public int Issuecardfee { get; set; }
        public int Issuecardmotorfee { get; set; }
        public int depositfee { get; set; }
        public int depositmotorfee { get; set; }
        public string precis1 { get; set; }
        public string Typecalculate { get; set; }
        public string singlestamptype { get; set; }
        public string Maxcardmembercar { get; set; }
        public string Maxcardmembermotor { get; set; }
        public string Branch { get; set; }
        public string ChangedataFee { get; set; }
        public string ChangedatamotorFee { get; set; }
        public string TurnstileAntipassback1 { get; set; }
        public string Stampdefine { get; set; }
        public string Buildingname { get; set; }
        public string Lprenable { get; set; }
        public string ParkingAntipassback { get; set; }
        public string onlineenable { get; set; }
        public string accesscctvflag { get; set; }
        public string Country { get; set; }
        public string CountryEng { get; set; }
        public string Sitenamereceipt { get; set; }
        public string Sitenamereport { get; set; }
        public string Stampsymbol { get; set; }
        public string Receipttype { get; set; }
        public int CarNumber { get; set; }
        public int MotorNumber { get; set; }
        public int CarSpace { get; set; }
        public int MotorSpace { get; set; }
        public string GPSLat { get; set; }
        public string GPSLong { get; set; }
        public List<SiteRegulation> siteRegulations { get; set; }
        public List<SiteRate> siteRates { get; set; }
        public List<SiteNearby> siteNearby { get; set; }
        public List<SiteContact> siteContact { get; set; }
        public List<SiteImage> siteImages { get; set; }
        public string siteDescriptionEn { get; set; }
        public string siteDescriptionTh { get; set; }
        public string key { get; set; }
        public object imageUrl { get; set; }
    }

    public class SiteContact
    {
        public string rowKey { get; set; }
        public string siteKey { get; set; }
        public string contactNameEn { get; set; }
        public string contactNameTh { get; set; }
        public string addressEn { get; set; }
        public string addressTh { get; set; }
        public string phone { get; set; }
        public object siteKeyNavigation { get; set; }
    }

    public class SiteImage
    {
        public string rowKey { get; set; }
        public string siteKey { get; set; }
        public string image { get; set; }
        public string descriptionEn { get; set; }
        public string descriptionTh { get; set; }
        public object siteKeyNavigation { get; set; }
    }

    public class SiteNearby
    {
        public int placeGroup { get; set; }
        public string placeIcon { get; set; }
        public string placeTypeEn { get; set; }
        public string placeTypeTh { get; set; }
        public List<Place> places { get; set; }
    }

    public class SiteRate
    {
        public string rowKey { get; set; }
        public string siteKey { get; set; }
        public string vehicleType { get; set; }
        public string hourlyRateEn { get; set; }
        public string hourlyRateTh { get; set; }
        public string dailyRateEn { get; set; }
        public string dailyRateTh { get; set; }
        public string monthlyRateEn { get; set; }
        public string monthlyRateTh { get; set; }
        public object siteKeyNavigation { get; set; }
    }

    public class SiteRegulation
    {
        public string rowKey { get; set; }
        public string siteKey { get; set; }
        public string descriptionEn { get; set; }
        public string descriptionTh { get; set; }
        public object siteKeyNavigation { get; set; }
    }

    public class Place
    {
        public string placeNameEn { get; set; }
        public string placeNameTh { get; set; }
    }
