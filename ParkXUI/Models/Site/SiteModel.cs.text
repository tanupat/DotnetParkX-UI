namespace ParkXUI.Models.Site;

public class SiteModel
{
    public string siteId { get; set; }
    public string siteName { get; set; }
    public string siteDescription { get; set; }
    public string siteType { get; set; } //  parkX or managedParkX 
    public string siteLogo { get; set; }
    public string siteImage { get; set; }
    public string siteMapImage { get; set; }
    public string siteMapUrl { get; set; }
    public List<SiteImage> siteImages { get; set; }
    public List<SiteParkServiceRegulation> siteParkServiceRegulations { get; set; }
    public List<SiteParkServiceRate> siteParkServiceRates { get; set; }
    public SiteContact siteContact { get; set; }
    public List<SiteNearbyPlace> siteNearbyPlaces { get; set; }
    
}

public class SiteContact
{
    public string contactName { get; set; }
    public string address { get; set; }
    public string contactPhone { get; set; }
}

public class SiteImage
{
    public string image { get; set; }
    public string description { get; set; }
}

public class SiteNearbyPlace
{
    public string placeType { get; set; }
    public string placeIcon { get; set; }
    public List<Place> places { get; set; }
}

public class SiteParkServiceRate
{
    public string typeOfVehicle { get; set; }
    public string HourlyRate { get; set; }
    public string MonthlyRate { get; set; }
}

public class SiteParkServiceRegulation
{
    public string regulation { get; set; }
}
public class Place
{
    public string placeName { get; set; }
}
