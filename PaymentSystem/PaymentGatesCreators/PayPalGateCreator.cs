using PayPal.Api;
using Sarnado.PaymentSystem;
using Sarnado.PaymentSystem.PaymentGates;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sarnado.PaymentSystem
{
    public class PayPalGateCreator : PaymantGateCreator
    {

        private string _clientId;
        private string _secret;

        public PayPalGateCreator(string clientId, string secret)
        {
            _clientId = clientId ?? throw new ArgumentNullException(nameof(clientId));
            _secret = secret ?? throw new ArgumentNullException(nameof(secret));
        }

        protected override IPaymantGate FactoryMethod()
        {
            return new PayPalGate(_clientId, _secret);
        }
    }
}
