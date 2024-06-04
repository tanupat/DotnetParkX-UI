namespace ParkXUI.Models.Auth;

public class UserModel
{
    public string token { get; set; }
    public string message { get; set; }
    public Data data { get; set; }
    public List<object> services { get; set; }
    public List<MemberVehicle> memberVehicle { get; set; }
    public string memberKey { get; set; }
    public bool isNewMember { get; set; }
    
}

public class Data
{
    public string wrapUserId { get; set; }
    public string lineId { get; set; }
    public string facebookId { get; set; }
    public string googleId { get; set; }
    public string microsoftId { get; set; }
    public string appleId { get; set; }
    public string openId { get; set; }
    public string phone { get; set; }
    public string fullName { get; set; }
    public string email { get; set; }
    public bool isVerified { get; set; }
    public string appToken { get; set; }
    public string mobilePin { get; set; }
    public DateTime createDate { get; set; }
    public List<Profile> profiles { get; set; }
}

public class MemberVehicle
{
    public string key { get; set; }
    public string memberKey { get; set; }
    public string licensePlate { get; set; }
    public string province { get; set; }
    public string maker { get; set; }
    public string model { get; set; }
    public string year { get; set; }
    public string color { get; set; }
    public string vehicleType { get; set; }
}

public class Profile
{
    public string key { get; set; }
    public string code { get; set; }
    public string taxid { get; set; }
    public string fullName { get; set; }
    public string address { get; set; }
    public string subDistrict { get; set; }
    public string district { get; set; }
    public string province { get; set; }
    public string zipCode { get; set; }
    public string phone { get; set; }
    public string fax { get; set; }
    public string email { get; set; }
    public string website { get; set; }
    public bool isVerified { get; set; }
    public string description { get; set; }
    public string registerOwner { get; set; }
    public int regType { get; set; }
    public string segment { get; set; }
}