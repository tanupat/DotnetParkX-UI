using ParkXUI.Models.Package;
using ParkXUI.Models.Site;

namespace ParkXUI.ViewModel.FindCarpark;

public class ParkingSitesViewModel
{
    //public SiteModel site { get; set; }
    public LocalizedSite sites { get; set; }
    public List<PackageModel>? packages { get; set; }
    
}