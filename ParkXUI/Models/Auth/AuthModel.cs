namespace ParkXUI.Models.Auth;

public class AuthModel
{
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string LineId { get; set; }
    public string FacebookId { get; set; }
    public string GoogleId { get; set; }
    public string OpenId { get; set; }
    public string AppleId { get; set; }
    public string MicrosoftId { get; set; }
    public string Name { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public bool? Register { get; set; }
    public string MemberKey { get; set; }
}