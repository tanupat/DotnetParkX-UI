using ParkXUI.Models.Package;

namespace ParkXUI.ViewModel.ParkingSites;

public class ComparePackagesViewModel
{
    public string SiteID { get; set; }
    public List<string> PackageIds { get; set; }
    
    public List<PackageModel>? packages { get; set; }
}