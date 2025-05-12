using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaSolvex.Infrastructure.Exceptions
{
    public class ProductVariationException : Exception
    {

        public ProductVariationException(string message) : base(message)
        {

        }

    }
}
