using ParkXUI.Models.PaymentModel;

namespace ParkXUI.Interfaces;

public interface IPaymentService
{
     Task<HttpResponseMessage> OrderAPI(OrderPaymentModel orderData);
     Task<HttpResponseMessage> ChargeAPI(ChargePostModel chargeData);
     Task<HttpResponseMessage> VoidPayment(string chargeId);
     Task<HttpResponseMessage> InquiryTransactionAPI(string chargeId);

}