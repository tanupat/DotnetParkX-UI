using System.Net;
using Newtonsoft.Json;
using ParkXUI.Interfaces;
using ParkXUI.Models.Site;
using ParkXUI.Utility;

namespace ParkXUI.Services;

public class SitesService:ISites
{
    private readonly HttpClientUtility _httpClientUtility;
    
    public SitesService(HttpClientUtility httpClientUtility)
    {
        _httpClientUtility = httpClientUtility;
    }
    

    public async Task<List<LocalizedSite>> GetSites(string lang, string siteId = null)
    {
        try
        {
            string url = string.IsNullOrEmpty(siteId) ? "Sites/Sites" : $"Sites/Sites?siteId={siteId}";
            List<LocalizedSite> _localizedSites = new List<LocalizedSite>();
            SiteList siteList = new SiteList();
            var response = await _httpClientUtility.GetAsync(url);
            if (response.HttpStatus == HttpStatusCode.OK)
            {
                siteList = JsonConvert.DeserializeObject<SiteList>(response.Data.ToString());
            }

        _localizedSites = siteList.Site.Select(site => new LocalizedSite
            {
                SiteID = site.SiteID,
                CompanyName = lang == "th" ? site.CompanyNameLocale : site.CompanyNameEng,
                Address = lang == "th" ? site.Addr1locale : site.Addr1eng,
                siteLogo = "https://www.parkx.com/uploads/location/20200507153605-2020-05-07location153604.png",
                siteMapImage = "https://www.parkx.com/uploads/find_carpark/20200512121337-2020-05-12find_carpark121336.jpg",
                siteMapUrl = $"https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d242.2890996179959!2d{site.GPSLong}!3d{site.GPSLat}!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x30e2a1efacf6a1b1%3A0xc6df25fe3fadb2a7!2z4Lie4Liy4Lij4LmM4LiE4LmA4Lit4LmH4LiB4LiL4LmMIOC4reC4uOC4lOC4oeC4quC4uOC4gg!5e0!3m2!1sth!2sth!4v1684464266552!5m2!1sth!2sth",
               // siteMapUrl = "https://www.google.com/maps/embed?pb=!1m14!1m8!1m3!1d242.2890996179959!2d100.6088276!3d13.6805158!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x30e2a1efacf6a1b1%3A0xc6df25fe3fadb2a7!2z4Lie4Liy4Lij4LmM4LiE4LmA4Lit4LmH4LiB4LiL4LmMIOC4reC4uOC4lOC4oeC4quC4uOC4gg!5e0!3m2!1sth!2sth!4v1684464266552!5m2!1sth!2sth",
                ZipCode = site.ZipCode,
                Tel = site.Tel,
                Fax = site.Fax,
                Vat = site.Vat,
                TaxFlg = site.TaxFlg,
                TaxType = site.TaxType,
                WithholdingTax = site.withholdingtax,
                DateMonthFare = site.DateMonthFare,
                DateHalfMonthFare = site.DateHalfMonthFare,
                IssueCardFee = site.Issuecardfee,
                IssueCardMotorFee = site.Issuecardmotorfee,
                DepositFee = site.depositfee,
                DepositMotorFee = site.depositmotorfee,
                Precis = site.precis1,
                TypeCalculate = site.Typecalculate,
                SingleStampType = site.singlestamptype,
                MaxCardMemberCar = site.Maxcardmembercar,
                MaxCardMemberMotor = site.Maxcardmembermotor,
                Branch = site.Branch,
                ChangeDataFee = site.ChangedataFee,
                ChangeDataMotorFee = site.ChangedatamotorFee,
                TurnstileAntipassback = site.TurnstileAntipassback1,
                StampDefine = site.Stampdefine,
                BuildingName = site.Buildingname,
                LprEnable = site.Lprenable,
                ParkingAntipassback = site.ParkingAntipassback,
                OnlineEnable = site.onlineenable,
                AccessCctvFlag = site.accesscctvflag,
                Country = lang == "th" ? site.Country : site.CountryEng,
                SiteNameReceipt = site.Sitenamereceipt,
                SiteNameReport = site.Sitenamereport,
                StampSymbol = site.Stampsymbol,
                ReceiptType = site.Receipttype,
                CarNumber = site.CarNumber,
                MotorNumber = site.MotorNumber,
                CarSpace = site.CarSpace,
                MotorSpace = site.MotorSpace,
                GPSLat = site.GPSLat,
                GPSLong = site.GPSLong,
                SiteRegulations = lang == "th" ? site.siteRegulations.Select(r => r.descriptionTh) : site.siteRegulations.Select(r => r.descriptionEn),
                siteDescription = lang == "th" ? site.siteDescriptionTh : site.siteDescriptionEn,
                SiteRates = site.siteRates.Select(r => new LocalizedSiteRate
                {
                    VehicleType = r.vehicleType,
                    HourlyRate = lang == "th" ? r.hourlyRateTh : r.hourlyRateEn,
                    DailyRate = lang == "th" ? r.dailyRateTh : r.dailyRateEn,
                    MonthlyRate = lang == "th" ? r.monthlyRateTh : r.monthlyRateEn
                }),
                SiteNearby = site.siteNearby.Select(n => new LocalizedSiteNearby
                {
                    PlaceGroup = n.placeGroup,
                    PlaceType = lang == "th" ? n.placeTypeTh : n.placeTypeEn,
                    PlaceIcon = n.placeIcon,
                    Places = n.places.Select(p => new LocalizedPlace
                    {
                        PlaceName = lang == "th" ? p.placeNameTh : p.placeNameEn
                    })
                }),
                SiteContact = site.siteContact.Select(c => new LocalizedSiteContact
                {
                    ContactName = lang == "th" ? c.contactNameTh : c.contactNameEn,
                    Address = lang == "th" ? c.addressTh : c.addressEn,
                    Phone = c.phone
                }),
                SiteImages = site.siteImages.Select(i => new LocalizedSiteImage
                {
                    Image = i.image,
                    Description = lang == "th" ? i.descriptionTh : i.descriptionEn
                })
            }).ToList();

            return _localizedSites;
        }catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}