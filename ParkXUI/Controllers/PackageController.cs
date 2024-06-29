using System.Globalization;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Package;
using ParkXUI.Models.PaymentModel;
using ParkXUI.Models.Vehicles;
using ParkXUI.Utility;
using ParkXUI.ViewModel.Package;

namespace ParkXUI.Controllers;

[Authorize(Policy = PolicyLogin.Member)]
public class PackageController : Controller
{
    private readonly HttpClientUtility _httpClientUtility;
    private readonly IPaymentService _paymentService;
    private readonly IAuth _authService;
    private readonly IConfiguration _configuration;
    private readonly ISites _sites;
    
    public PackageController(HttpClientUtility httpClientUtility, IAuth authService, IPaymentService paymentService, IConfiguration configuration,ISites sites)
    {
        _httpClientUtility = httpClientUtility;
        _authService = authService;
        _paymentService = paymentService;
        _configuration = configuration;
        _sites = sites;
    }
    
    
    public async Task<IActionResult> PackageDetail(string packageId,string siteId = null)
    {
        PackageDetailViewModel packageDetailViewModel = new PackageDetailViewModel();
        var apiPackageResult = await _httpClientUtility.GetAsync("Packages/Packages?packageIdOrKey=" + packageId);
        var lang = CultureInfo.CurrentCulture.Name;
        if (apiPackageResult.HttpStatus == HttpStatusCode.OK)
        {
           var sites = await _sites.GetSites(lang, siteId);
            packageDetailViewModel.sites = sites.FirstOrDefault();
            var package = JsonConvert.DeserializeObject<List<PackageModel>>(apiPackageResult.Data);
            packageDetailViewModel.Package = package.FirstOrDefault();
            //    packageDetailViewModel.Package = package;
        }
        
        return View(packageDetailViewModel);
    }
    
    public async Task<IActionResult> PurchasePackage(string packageId, string name = null, string packageType = null, string price = null, string chargeId = null,string token = null, string mid = null, string paymentMethods = null, string selectedVehicles = null, string maxCarData = null, string coCurrentData = null, string startDate = null, string endDate = null, string amount = null, string currency = null, string refNumber = null, string orderId = null, string ref_1 = null, string ref_2 = null, string paymentType = null)
    { 
        //QR
       //https://localhost:7077/Package/PurchasePackage?packageId=552403ec-7735-48e4-981c-a0c942d7afe9&selectedVehicles=&maxCarData=&coCurrentData=&name=Coprate10001&packageType=Daily&price=16000&chargeId=chrg_test_2205427b0727cea46454da876c2a6242d9f4e
        //Card
       //https://localhost:7077/Package/PurchasePackage?packageId=552403ec-7735-48e4-981c-a0c942d7afe9&selectedVehicles=&maxCarData=&coCurrentData=&name=Coprate10001&packageType=Daily&price=15000&amount=15000&currency=THB&payment-methods=card&name=Coprate10001+PXM20245631&mid=401617144789001&ref-number=ODR20240600031&order-id=&ref_1=ODR20240600031&ref_2=PKXC10001&token=tokn_test_220546ba9d51d6d45ffd7415339ee18b548ba&mid=401617144789001&paymentMethods=card
        PurchasePackageViewModel purchasePackageViewModel = new PurchasePackageViewModel();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userModel =  _authService.GetUserDetail(userId).Result;
        purchasePackageViewModel.userId = userId;
        
        var apiPackageResult = await _httpClientUtility.GetAsync("Packages/Packages?packageIdOrKey=" + packageId);
        purchasePackageViewModel.MemberVehicles = userModel.memberVehicle;
        if (apiPackageResult.HttpStatus == HttpStatusCode.OK)
        {
            var package = JsonConvert.DeserializeObject<List<PackageModel>>(apiPackageResult.Data);
            purchasePackageViewModel.Package = package.FirstOrDefault();
           
        }

        if (token != null && chargeId == null)
        {
            var chargeData = new ChargePostModel()
            {
                amount = Convert.ToInt32(price),
                currency = "THB",
                description = purchasePackageViewModel.Package.PackageName,
                source_type = paymentMethods,
                mode = "token",
                token = token,
                reference_order = ref_1,
                ref_1 = ref_1,
                ref_2 = ref_2,
            };
          var  dataChargeAPI =  await _paymentService.ChargeAPI(chargeData);
          string json =   dataChargeAPI.Content.ReadAsStringAsync().Result;
          ChargePostResultApi chargePostResultApi = JsonConvert.DeserializeObject<ChargePostResultApi>(json);

          return Redirect(chargePostResultApi.redirect_url);

        }
        else if(token == null && chargeId != null)
        {
            //
            var dataInquiryTransactionAPI = await _paymentService.InquiryTransactionAPI(chargeId);
            string json =   dataInquiryTransactionAPI.Content.ReadAsStringAsync().Result;
            ChargePostResultApi chargePostResultApi = JsonConvert.DeserializeObject<ChargePostResultApi>(json);
            if (chargePostResultApi.status == "success")
            {
                return RedirectToAction("PaymentSuccess", "Package");
              //ViewBag.error = "Payment failed. Please try again.";
            }
            else
            {
                ViewBag.error = "Payment failed. Please try again.";
            }
        }
        

        return View(purchasePackageViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> PurchasePackage()
    {
        //get request data
        var dataRequest = HttpContext.Request.Form;
        // Convert form data to a dictionary
        var formData = dataRequest.ToDictionary(x => x.Key, x => x.Value.ToString());

        // Serialize dictionary to JSON
        var json = JsonConvert.SerializeObject(formData);
        
        var packageId = formData.ContainsKey("packageId") ? formData["packageId"] : null;
        var maxCarData = formData.ContainsKey("maxCarData") ? formData["maxCarData"] : null;
        var coCurrentData = formData.ContainsKey("coCurrentData") ? formData["coCurrentData"] : null;
        var startDate = formData.ContainsKey("startDate") ? formData["startDate"] : null;
        var selectedVehicles = formData.ContainsKey("selectedVehicles") ? formData["selectedVehicles"] : null;
        var paymentType = formData.ContainsKey("paymentType") ? formData["paymentType"] : null;
        var price = formData.ContainsKey("price") ? formData["price"].Replace(",", ""): null;
        var endDate = formData.ContainsKey("endDate") ? formData["endDate"] : null;
        
        
        List<VehicleModel> vehicles = new List<VehicleModel>();
        if (selectedVehicles != null)
        {
            vehicles = JsonConvert.DeserializeObject<List<VehicleModel>>(selectedVehicles);
        }
        
        PurchasePackageViewModel purchasePackageViewModel = new PurchasePackageViewModel();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userModel =  _authService.GetUserDetail(userId).Result;
        
        var apiPackageResult = await _httpClientUtility.GetAsync("Packages/Packages?packageIdOrKey=" + packageId);
        purchasePackageViewModel.MemberVehicles = userModel.memberVehicle;
        
        var package = JsonConvert.DeserializeObject<List<PackageModel>>(apiPackageResult.Data);
        if (apiPackageResult.HttpStatus == HttpStatusCode.OK)
        {
            purchasePackageViewModel.Package = package.FirstOrDefault();
        }
        var dataKBankGateway = new KBankGatewayModel()
        {
            memberIdOrKey = userId,
            packageIdOrKey = purchasePackageViewModel.Package.key,
            qty = 1,
            startDate = startDate,
            maxCar = maxCarData != null ? Convert.ToInt32(maxCarData) : 0,
            coCurrent = coCurrentData != null ? Convert.ToInt32(coCurrentData) : 0,
            car = vehicles

        };
        string DataJson = JsonConvert.SerializeObject(dataKBankGateway);
        var apiKBankGatewayResult = await _httpClientUtility.PostAsync("Payments/KBankGatewayCreateCharge", DataJson);
        if(apiKBankGatewayResult.HttpStatus == HttpStatusCode.OK)
        {
              KBankGatewayResultModel  kBankGatewayResultModel = JsonConvert.DeserializeObject<KBankGatewayResultModel>(apiKBankGatewayResult.Data);

              var orderParameterModel = new OrderPaymentModel()
              {
                  amount = kBankGatewayResultModel.amount,
                  currency = "THB",
                  description = package[0].PackageName,
                  source_type = paymentType,
                  ref_1 = package[0].PackageID,
                  ref_2 = userModel.data.wrapUserId,
                  reference_order = kBankGatewayResultModel.orderId
              };
              if (paymentType == "card")
              {
                  PaymentPayloadModel paymentPayloadModel = new PaymentPayloadModel()
                  {
                      amount = price,
                      currency = "THB",
                      paymentMethods = paymentType,
                      name = package[0].PackageName+" "+ userModel.data.wrapUserId,
                      mid = _configuration.GetSection("Payment:PaymentMID").Value,
                      ref_1 = kBankGatewayResultModel.orderId,
                      ref_2 = package[0].PackageID,
                      apikey =  _configuration.GetSection("Payment:PaymentPublicKey").Value,
                      srcScript = _configuration.GetSection("Payment:PaymentJavaScriptApi").Value,
                      refNumber = kBankGatewayResultModel.orderId
                    
                  };
                  purchasePackageViewModel.PaymentPayload = paymentPayloadModel;
                  purchasePackageViewModel.SummaryPayment = new SummaryPayment()
                  {
                      PackageName = package[0].PackageName,
                      PackageType = package[0].PackageType,
                      price = price,
                      startDate = startDate,
                      endDate = endDate,
                      maxCar = maxCarData,
                      vehicles = vehicles,
                      coCurrent = coCurrentData,
                      PaymentMethod = paymentType
                  };
                  
              }else if (paymentType == "qr")
              {
                var  dataOrderApi = await  _paymentService.OrderAPI(orderParameterModel);
                var resultOrder = JsonConvert.DeserializeObject<OrderPaymentResult>(dataOrderApi.Content.ReadAsStringAsync().Result);
                 PaymentPayloadModel paymentPayloadModel = new PaymentPayloadModel()
                 {
                    amount = price,
                    orderId = resultOrder.id,
                    currency = "THB",
                    paymentMethods = paymentType,
                    name = package[0].PackageName+" "+ userModel.data.wrapUserId,
                    mid = _configuration.GetSection("Payment:PaymentMID").Value,
                    ref_1 = kBankGatewayResultModel.orderId,
                    ref_2 = package[0].PackageID,
                    apikey =  _configuration.GetSection("Payment:PaymentPublicKey").Value,
                    srcScript = _configuration.GetSection("Payment:PaymentJavaScriptApi").Value,
                    refNumber = kBankGatewayResultModel.orderId
                    
                 };
                 purchasePackageViewModel.PaymentPayload = paymentPayloadModel;
                 purchasePackageViewModel.SummaryPayment = new SummaryPayment()
                 {
                     PackageName = package[0].PackageName,
                     PackageType = package[0].PackageType,
                     price = price,
                     startDate = startDate,
                     endDate = endDate,
                     maxCar = maxCarData,
                     vehicles = vehicles,
                     coCurrent = coCurrentData,
                     PaymentMethod = paymentType
                 };
              }
        }
        else
        {
            ViewBag.error = "Payment failed. Please try again.";
        }
        return View(purchasePackageViewModel);
    }
    
    public IActionResult PackageMember()
    {
        PackageMemberViewModel packageMemberViewModel = new PackageMemberViewModel();
        var userId =   HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
      
        
        var  packageUser = _httpClientUtility.GetAsync("Packages/PackageMember?memberIdOrKey="+userId).Result;

        if (packageUser.HttpStatus == HttpStatusCode.OK)
        {
            packageMemberViewModel.PackageMembers = JsonConvert.DeserializeObject<List<PackageMemberModel>>(packageUser.Data);
        }
        
        return View(packageMemberViewModel);
    }

    [AllowAnonymous]
    public IActionResult PaymentSuccess( string orderId = null)
    {
        return View();
    }



}