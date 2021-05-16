using System;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Sarnado.PaymentSystem.PaymentGates
{
    public class PayPalGate : IPaymantGate
    {

        private string _clientId;
        private string _secret;
        private PayPalHttpClient _client;
   
        public PayPalGate(string clienntId, string secret)
        {
            _clientId = clienntId;
            _secret = secret;


            PayPalEnvironment environment = new PayPalEnvironment(_clientId, _secret, "https://api-m.paypal.com", "https://sarnado.club/");

            // Creating a client for the environment
            _client = new PayPalHttpClient(environment);

          
        }

        
        public Task<IPaymentReceipt> GetPaymentReceipt()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPaymentStatus()
        {
            throw new NotImplementedException();
        }

        public async Task<IPaymentReceipt> ProvidePayment(IPaymentRequest paymentRequest)
        {
           await createOrder(paymentRequest);
            
            return null;
        }

        public async Task<Order> CheckOrder(string orderId) 
        {

            var order = await CaptureOrder(orderId, false);
            return order;
        }

        private async  Task<Order> CaptureOrder(string OrderId, bool debug = false)
        {
            var request = new OrdersCaptureRequest(OrderId);
            request.Prefer("return=representation");
            request.RequestBody(new OrderActionRequest());
            var response = await _client.Execute(request);
            var result = response.Result<Order>();
            

            if (debug)
            {
                Console.WriteLine("Status: {0}", result.Status);
                Console.WriteLine("Order Id: {0}", result.Id);
                Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
                Console.WriteLine("Links:");
                foreach (LinkDescription link in result.Links)
                {
                    Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
                }
                Console.WriteLine("Capture Ids: ");
                foreach (PurchaseUnit purchaseUnit in result.PurchaseUnits)
                {
                    foreach (Capture capture in purchaseUnit.Payments.Captures)
                    {
                        Console.WriteLine("\t {0}", capture.Id);
                    }
                }
                AmountWithBreakdown amount = result.PurchaseUnits[0].AmountWithBreakdown;
                Console.WriteLine("Buyer:");
                Console.WriteLine("\tEmail Address: {0}\n\tName: {1} {2}\n",
                    result.Payer.Email,
                    result.Payer.Name.GivenName,
                    result.Payer.Name.Surname);
                
            }

            return result;
        }
        private async Task<HttpResponse> createOrder(IPaymentRequest paymentRequest)
        {
            HttpResponse response;
            // Construct a request object and set desired parameters
            // Here, OrdersCreateRequest() creates a POST request to /v2/checkout/orders
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                    new PurchaseUnitRequest()
                    {

                        AmountWithBreakdown = new AmountWithBreakdown()
                        {
                            CurrencyCode = paymentRequest.Currency,
                            Value = paymentRequest.Amount
                        }
                    }
                },
                ApplicationContext = new ApplicationContext()
                {
                    ReturnUrl = paymentRequest.ReturnUrl,
                    CancelUrl = paymentRequest.CancelUrl
                }
            };


            // Call API with your client and get a response for your call
            var request = new OrdersCreateRequest();
            request.Prefer("return=representation");
            request.RequestBody(order);
            response = await _client.Execute(request);
            var statusCode = response.StatusCode;
            Order result = response.Result<Order>();
            Console.WriteLine("Status: {0}", result.Status);
            Console.WriteLine("Order Id: {0}", result.Id);
            Console.WriteLine("Intent: {0}", result.CheckoutPaymentIntent);
            Console.WriteLine("Links:");
            foreach (LinkDescription link in result.Links)
            {
                Console.WriteLine("\t{0}: {1}\tCall Type: {2}", link.Rel, link.Href, link.Method);
            }
            return response;
        }



    }
}
