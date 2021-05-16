using Sarnado.PaymentSystem.PaymentGates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sarnado.PaymentSystem
{
    public abstract class PaymantGateCreator : IPaymantGateCreator
    {
        protected abstract IPaymantGate FactoryMethod();

        public async Task<IPaymentReceipt> CreatePayment(IPaymentRequest paymentRequest)
        {
            var paymentGate = FactoryMethod();

            return await paymentGate.ProvidePayment(paymentRequest);
        }

        public async Task<string> ConfirmPayment(IConfirmPaymentRequest confirmPaymentRequest)
        {
            var paymentGate = FactoryMethod();
            var order = await paymentGate.CheckOrder(confirmPaymentRequest.OrderId);

            return order.Id;
        }


    }






}
