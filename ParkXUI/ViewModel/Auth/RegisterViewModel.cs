using System.ComponentModel.DataAnnotations;

namespace ParkXUI.ViewModel.Auth;

public class RegisterViewModel
{
    [Required]
    [EmailAddress]
    public string email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("password", ErrorMessage = "Your password and confirm password do not match")]
    public string ConfirmPassword { get; set; }
    
    [Required]
    public string fullName { get; set; }

}