using System.ComponentModel.DataAnnotations;

namespace ParkXUI.ViewModel.User;

public class ProfileModel
{
  
     public string memberKey { get; set; }
     public string wrapUserId { get; set; }
     [Required]
     public string fullName { get; set; }
     public string phone { get; set; }
     public string email { get; set; }
     public string address { get; set; }
     public string district { get; set; }
     public string subDistrict { get; set; }
     public string province { get; set; }
     public string zipCode { get; set; }
}