using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Exceptions
{
    public class ProductNotFoundServiceException : Exception
    {
        public ProductNotFoundServiceException()
            : base()
        { }

        public ProductNotFoundServiceException(string message)
            : base(message)
        { }

        public ProductNotFoundServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
