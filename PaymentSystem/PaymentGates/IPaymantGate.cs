using PayPalCheckoutSdk.Orders;
using System.Threading.Tasks;

namespace Sarnado.PaymentSystem.PaymentGates
{
    public interface IPaymantGate
    {
        public Task<IPaymentReceipt> ProvidePayment(IPaymentRequest paymentRequest);
        public Task<IPaymentReceipt> GetPaymentReceipt();
        public Task<string> GetPaymentStatus();
        public Task<Order> CheckOrder(string orderId);
    }
}