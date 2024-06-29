namespace ParkXUI.Models.User;

public class UserDetailModel
{
    public string key { get; set; }
    public string fullName { get; set; }
    public string address { get; set; }
    public string subDistrict { get; set; }
    public string district { get; set; }
    public string province { get; set; }
    public string zipCode { get; set; }
    public string phone { get; set; }
    public string email { get; set; }
    public List<Address> addresses { get; set; }
    public bool update { get; set; }
}

public class Address
{
    public int typeId { get; set; }
    public string address { get; set; }
    public string subDistrict { get; set; }
    public string district { get; set; }
    public string province { get; set; }
    public string zipCode { get; set; }
    public string phone { get; set; }
}