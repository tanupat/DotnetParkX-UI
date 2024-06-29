using ParkXUI.Models.Package;
using ParkXUI.Models.Site;

namespace ParkXUI.ViewModel.Package;

public class PackageDetailViewModel
{
    public LocalizedSite sites { get; set; }
    public PackageModel Package { get; set; }
}