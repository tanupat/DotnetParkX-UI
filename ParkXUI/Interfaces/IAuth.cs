using ParkXUI.Models.Auth;

namespace ParkXUI.Interfaces;

public interface IAuth
{
     Task<UserModel>GetUserDetail(string token);
}