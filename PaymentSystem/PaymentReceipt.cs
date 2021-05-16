using System;
using System.Collections.Generic;
using System.Text;

namespace Sarnado.PaymentSystem
{
    public class PaymentReceipt : IPaymentReceipt
    {
        public PaymentReceipt(string userName, string orderId, string status, decimal amount)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            OrderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            Status = status ?? throw new ArgumentNullException(nameof(status));
            Amount = amount > 0 ? Amount : throw new Exception("Amount must be not 0");
        }

        public string UserName { get; }
        public string OrderId { get; }
        public string Status { get; }
        public decimal Amount { get; }
    }
}
