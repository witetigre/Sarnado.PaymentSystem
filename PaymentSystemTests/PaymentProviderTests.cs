using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sarnado.PaymentSystem;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentSystem.Tests
{
    [TestClass()]
    public class PaymentProviderTests
    {
        [TestMethod()]
        public void CreatePaymentTest()
        {

            var paymentProvider = new PaymentProvider();
            _ = paymentProvider.CreatePayment(
                new PayPalGateCreator(
                    "AXNj21J1Y3fKIyAcVIjmuE7Zw-BgRfhRu5Pa-8Ycwjh59U6k7YCy3CYzaxs1PvY_Yb17_3Gdsh__xZhp",
                    "EIJhlZhr7UK3V8egddgtanRyk0PoC71omkfWT05yhsufAKZIdHtSRZdaMysDy5mQ0ybqUfTSq_651pzj"
                ),
                new PaymentRequest("white", "USD", "100,0") {
                    ReturnUrl = "https://sarnado.club/",
                    CancelUrl = "https://sarnado.club/"
                }
                ).Result;
        }
        private class PaymentRequest : IPaymentRequest
        {
            public PaymentRequest(string userName, string currency, string amount)
            {
                UserName = userName ?? throw new ArgumentNullException(nameof(userName));
                Currency = currency ?? throw new ArgumentNullException(nameof(currency));
                Amount   = amount ?? throw new ArgumentNullException(nameof(amount));
            }

            public string UserName { get; }

            public string Currency { get; }

            public string Amount { get; }

            public string ReturnUrl { get; set; }

            public string CancelUrl { get; set; }

        }
    }

}