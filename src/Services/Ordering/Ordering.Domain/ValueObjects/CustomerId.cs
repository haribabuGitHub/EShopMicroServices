namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Value { get; private set; }

        private static CustomerId Of(Guid value)
        {
            ArgumentNullException.ThrowIfNull(value);

            if (value == Guid.Empty)
            {
                throw new DomainExceptions("CustomerId cannot be empty.");
            }

            return new CustomerId { Value = value };
        }
    }
}
