using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {
        private const int DefaultLength = 5;
        public string Value { get; } = string.Empty;

        private OrderName(string value)
        {
            Value = value;
        }

        public static OrderName Of(string value)
        {
            ArgumentNullException.ThrowIfNull(value);
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainExceptions("OrderName cannot be empty.");
            }
            if (value.Length > DefaultLength)
            {
                throw new DomainExceptions($"OrderName cannot be longer than {DefaultLength} characters.");
            }
            return new OrderName(value);
        }
    }
}
