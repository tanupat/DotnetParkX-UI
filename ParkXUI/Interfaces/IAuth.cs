using ParkXUI.Models.Auth;
using ParkXUI.Models.User;

namespace ParkXUI.Interfaces;

public interface IAuth
{
     Task<UserModel>GetUserDetail(string token);
     
     Task<bool> UpdateProfile(UserDetailModel userDetail);
}