using System;
using System.Collections.Generic;
using System.Text;

namespace Sarnado.PaymentSystem
{
    public interface IPaymentRequest
    {
        public string UserName { get; }
        public string Currency { get; }
        public string Amount { get; }
        public string ReturnUrl { get; }
        public string CancelUrl { get; }

    }
}
