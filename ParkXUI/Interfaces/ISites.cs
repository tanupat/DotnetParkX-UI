using ParkXUI.Models.Site;

namespace ParkXUI.Interfaces;

public interface ISites
{
    Task<List<LocalizedSite>> GetSites(string lang,string siteId = null);
}