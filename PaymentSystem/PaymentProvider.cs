using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sarnado.PaymentSystem
{
    public class PaymentProvider : IPaymentProvider
    {
        public async Task<string> ConfirmPayment(IPaymantGateCreator paymantGateCreator, IConfirmPaymentRequest paymentRequest)
        {
            return await paymantGateCreator.ConfirmPayment(paymentRequest);
        }

        public async Task<IPaymentReceipt> CreatePayment(IPaymantGateCreator paymantGateCreator, IPaymentRequest paymentRequest)
        {
            return await paymantGateCreator.CreatePayment(paymentRequest);
        }
    }
}
