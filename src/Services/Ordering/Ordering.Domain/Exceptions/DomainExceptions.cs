namespace Ordering.Domain.Exceptions
{
    public class DomainExceptions : Exception
    {
        public DomainExceptions(string message) : base($" {message} throw from doamin layer")
        {
        }
    }
}
