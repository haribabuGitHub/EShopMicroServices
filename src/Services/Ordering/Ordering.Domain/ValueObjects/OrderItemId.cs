using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Guid { get; }

        private OrderItemId(Guid guid)
        {
            Guid = guid;
        }

        public static OrderItemId Of(Guid guid)
        {
            ArgumentNullException.ThrowIfNull(guid);
            if (guid == Guid.Empty)
            {
                throw new DomainExceptions("OrderItemId cannot be empty.");
            }
            return new OrderItemId(guid);
        }
    }
}
