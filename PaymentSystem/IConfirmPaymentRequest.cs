namespace Sarnado.PaymentSystem
{
    public interface IConfirmPaymentRequest
    {
        public string OrderId { get; }
    }
}