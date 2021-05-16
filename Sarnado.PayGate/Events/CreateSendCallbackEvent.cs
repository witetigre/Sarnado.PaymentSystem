using CallBackSenders.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sarnado.PayGate.Events
{
    public class CreateSendCallbackEvent
    {
        delegate void OnPaymentConfirm(string orderId);
        event OnPaymentConfirm PaymentConfirm;
        private PayPalCallbackSender _callbackSender;
        public CreateSendCallbackEvent(PayPalCallbackSender callbackSender)
        {
            PaymentConfirm += CreateSendCallbackEvent_PaymentConfirm;
            _callbackSender = callbackSender;
        }

        public void InvokeCallbackEvent(string orderId) 
        {
            PaymentConfirm?.Invoke(orderId);
        }
        private async void CreateSendCallbackEvent_PaymentConfirm(string orderId)
        {
            await _callbackSender.SendCallBack();
        }
    }
}
