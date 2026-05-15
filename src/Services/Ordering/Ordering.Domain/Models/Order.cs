using Ordering.Domain.Abstractions;
using Ordering.Domain.ValueObjects;
using Ordering.Domain.ValueObjects.Enums;

namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderId>
    {
        private readonly List<OrderItem> _orderItems = new();

        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

        public CustomerId CustomerId { get; private set; }

        public OrderName OrderName { get; private set; } 

        public Address ShippingAddress { get; private set; } = new Address();

        public Address BillingAddress { get; private set; } = new Address();

        public OrderStatus Status { get; private set; } = OrderStatus.Pending;

        public decimal TotalAmount {
           
            get { return _orderItems.Sum(item => item.Quantity * item.Price); } 

            private set { }
        }
    }
}
