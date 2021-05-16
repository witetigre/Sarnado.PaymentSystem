using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sarnado.PaymentSystem
{
    public interface IPaymentProvider
    {
        public Task<IPaymentReceipt> CreatePayment(IPaymantGateCreator paymantGateCreator, IPaymentRequest paymentRequest);

        public Task<string> ConfirmPayment(IPaymantGateCreator paymantGateCreator, IConfirmPaymentRequest paymentRequest);
    }
}
