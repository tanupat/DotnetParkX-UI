using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.PaymentModel;
using ParkXUI.Utility;

namespace ParkXUI.Services;

public class PaymentService:IPaymentService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly HttpClientUtility _httpClientUtility;
    
    
    public PaymentService(IConfiguration configuration)
    {
      
        _configuration = configuration;
        _httpClient = new HttpClient(); // Initialize the HttpClient
       string  _urlAPI = _configuration.GetSection("Payment:PaymentApiUrl").Value;
       string  _xApiKey = _configuration.GetSection("Payment:PaymentSecretKey").Value;
        
        _httpClient.BaseAddress = new Uri(_urlAPI);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _xApiKey); 
    }
   
    public async Task<HttpResponseMessage> OrderAPI(OrderPaymentModel orderData)
    {
        var content = new StringContent(JsonConvert.SerializeObject(orderData), Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("qr/v2/order", content);
    }

    public async Task<HttpResponseMessage> ChargeAPI(ChargePostModel chargeData)
    {
        var content = new StringContent(JsonConvert.SerializeObject(chargeData), Encoding.UTF8, "application/json");
        return await _httpClient.PostAsync("card/v2/charge", content);
    }

    public async Task<HttpResponseMessage> VoidPayment(string chargeId)
    {
        return await _httpClient.PostAsync($"qr/v2/qr/{chargeId}/void", null);
    }

   

    public async Task<HttpResponseMessage> InquiryTransactionAPI(string chargeId)
    {
        return await _httpClient.GetAsync($"qr/v2/qr/{chargeId}");
    }
}