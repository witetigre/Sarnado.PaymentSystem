using System.Threading.Tasks;

namespace Sarnado.PaymentSystem
{
    public interface IPaymantGateCreator
    {
        public Task<IPaymentReceipt> CreatePayment(IPaymentRequest paymentRequest);
        public Task<string> ConfirmPayment(IConfirmPaymentRequest confirmPaymentRequest);
    }
}