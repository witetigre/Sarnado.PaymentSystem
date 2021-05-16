using System;
using System.Collections.Generic;
using System.Text;

namespace Sarnado.PaymentSystem
{
    public interface IPaymentReceipt
    {
        public string UserName { get; }
        public string OrderId { get; }
        public string Status { get; }
        public decimal Amount { get; }
    }
}
