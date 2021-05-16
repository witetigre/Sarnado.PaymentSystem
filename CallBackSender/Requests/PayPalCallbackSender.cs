using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CallBackSenders;

namespace CallBackSenders.Requests
{


    public class PayPalCallbackSender : CallbackSender
    {
        private string _orderId;
        public PayPalCallbackSender(string url, string orderId) : base(url)
        {
            _orderId = orderId;
        }
        protected override object GetCallBackData()
        {
            return _orderId;
        }
    }
}
