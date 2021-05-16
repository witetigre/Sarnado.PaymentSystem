using CallBackSenders;
using CallBackSenders.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Sarnado.PayGate.Events;
using Sarnado.PaymentSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sarnado.PayGate.Controllers
{
    [Route("api/[controller]")] // api/Librarian
    [ApiController]
    public class PaymentController : Controller
    {
        private IPaymentProvider _paymentProvider;
        private IConfiguration _configuration;
        public PaymentController(IPaymentProvider paymentProvider, IConfiguration configuration)
        {
            _paymentProvider = paymentProvider;
            _configuration = configuration;


        }

        /// <summary>
        /// Создание платежа
        /// </summary>
        /// <param name="paymentHttpRequest"></param>
        /// <returns></returns>
        [HttpPost("/add", Name = "AddPaymet")]
        public async Task<IActionResult> AddPaymet(PaymentHttpRequest paymentHttpRequest)
        {
            if (!ModelState.IsValid) { throw new Exception("Bad request"); }

            var clientId  = _configuration.GetSection("PayPalCredentials").GetValue<string>("ClientId");
            var secret    = _configuration.GetSection("PayPalCredentials").GetValue<string>("Secret");
            var returnUrl = _configuration.GetSection("PayPalCredentials").GetValue<string>("ReturnUrl");
            var cancelUrl = _configuration.GetSection("PayPalCredentials").GetValue<string>("CancelUrl");

            var paymentReceipt = await _paymentProvider.CreatePayment(
                    new PayPalGateCreator(
                        clientId, // TODO: Config
                        secret
                    ),
                    new PaymentRequest(paymentHttpRequest.User, paymentHttpRequest.Currency, paymentHttpRequest.Amount)
                    {
                        ReturnUrl = returnUrl, //TODO: Config
                        CancelUrl = cancelUrl
                    }
                );
            return Ok(paymentReceipt);
        }

        /// <summary>
        /// Confirm order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns>OrderId if confirmed</returns>
        [HttpGet("/confirmation", Name = "Confirmation")]
        public async Task<IActionResult> Confirmation(string orderId)
        {
            var clientId = _configuration.GetSection("PayPalCredentials").GetValue<string>("ClientId");
            var secret = _configuration.GetSection("PayPalCredentials").GetValue<string>("Secret");
            var callbackUrl = _configuration.GetSection("PayPalCredentials").GetValue<string>("SarnadoCallbackUrl");
           

            var paymentReceipt = await _paymentProvider.ConfirmPayment(
                new PayPalGateCreator(
                    clientId,
                    secret
                ),
                 new ConfirmPaymentRequest(orderId)
                );

            var callbackEvent = new CreateSendCallbackEvent(new PayPalCallbackSender(callbackUrl, paymentReceipt));
            callbackEvent.InvokeCallbackEvent(paymentReceipt);
            //Send callback
            return Ok(paymentReceipt);
        }
        private class PaymentRequest : IPaymentRequest
        {
            public PaymentRequest(string userName, string currency, string amount)
            {
                UserName = userName ?? throw new ArgumentNullException(nameof(userName));
                Currency = currency ?? throw new ArgumentNullException(nameof(currency));
                Amount   = amount ?? throw new ArgumentNullException(nameof(amount));
            }

            public string UserName { get;  }

            public string Currency { get; }

            public string Amount { get; }

            public string ReturnUrl { get; set; }

            public string CancelUrl { get; set; }
        }

        private class ConfirmPaymentRequest : IConfirmPaymentRequest
        {
            public ConfirmPaymentRequest(string orderId)
            {
                OrderId = orderId ?? throw new ArgumentNullException(nameof(orderId));
            }

            public string OrderId { get; }
        }
    }
    public class PaymentHttpRequest
    {
        [Required]
        [MaxLength(100)]
        public string Amount { get; set; }
        [Required]
        [MaxLength(5)]
        public string Currency { get; set; }
        /// <summary>
        /// Платежный шлюз (PayPal например)
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string PaymentGate { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string User { get; set; }
    }


}
