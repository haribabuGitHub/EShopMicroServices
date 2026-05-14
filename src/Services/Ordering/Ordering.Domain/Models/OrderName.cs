using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Models
{
    public record OrderName
    {
        public string Value { get; } = string.Empty;
    }
}
