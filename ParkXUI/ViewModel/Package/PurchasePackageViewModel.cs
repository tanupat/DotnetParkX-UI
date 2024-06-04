using ParkXUI.Models.Auth;
using ParkXUI.Models.Package;
using ParkXUI.Models.PaymentModel;

namespace ParkXUI.ViewModel.Package;

public class PurchasePackageViewModel
{
    public PackageModel Package { get; set; }
    public List<MemberVehicle> MemberVehicles { get; set; }
    
    public SummaryPayment SummaryPayment { get; set; }
    public PaymentPayloadModel PaymentPayload { get; set; }
}