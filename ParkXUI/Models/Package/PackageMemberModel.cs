namespace ParkXUI.Models.Package;

public class PackageMemberModel
{
    public string orderId { get; set; }
    public string packageId { get; set; }
    public string packageName { get; set; }
    public string expireDate { get; set; }
    public string deleted { get; set; }
    public int active { get; set; }
    public string updatedate { get; set; }
    public string headerText { get; set; }
    public string subText { get; set; }
    public string contentText { get; set; }
    public List<PackageModel> package { get; set; }
    public string key { get; set; }
    public string imageUrl { get; set; }
}