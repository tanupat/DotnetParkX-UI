using System.Net;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Auth;
using ParkXUI.Utility;

namespace ParkXUI.Services;

public class AuthService : IAuth
{
    private readonly HttpClientUtility _httpClientUtility;

    public AuthService(HttpClientUtility httpClientUtility)
    {
        _httpClientUtility = httpClientUtility;
    }

    public async Task<UserModel> GetUserDetail(string token)
    {
        try
        {
            
            UserModel userModel = new UserModel();

            var data = new
            {
                memberKey = token,
                register = true
            };
            
            var response = await _httpClientUtility.PostAsync("auth/ValidateMember", data);
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                userModel = JsonConvert.DeserializeObject<UserModel>(response.Data);
            }
            else
            {
                userModel.message = response.MessageError;
            }
            
            return userModel;
        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }
    }
}