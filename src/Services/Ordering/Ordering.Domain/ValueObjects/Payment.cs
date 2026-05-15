namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string CardNumber { get; init; } = default!;
        public string CardName { get; init; } = default!;

        public string Expiration { get; init; } = default!;

        public string CVV { get; init; } = default!;

        public int PaymentMethod { get; init; }

        protected Payment() { }

        private Payment(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
        {
            CardNumber = cardNumber;
            CardName = cardName;
            Expiration = expiration;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardNumber, string cardName, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber, nameof(cardNumber));
            ArgumentException.ThrowIfNullOrEmpty(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrEmpty(expiration, nameof(expiration));
            ArgumentException.ThrowIfNullOrEmpty(cvv, nameof(cvv));
            if (paymentMethod < 0)
            {
                throw new DomainExceptions("PaymentMethod must be a non-negative integer.");
            }
            return new Payment(cardNumber, cardName, expiration, cvv, paymentMethod);
        }
    }
}
