using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Exceptions
{
    public class ProductRepositoryCreateException : Exception
    {
        public ProductRepositoryCreateException()
            : base()
        { }

        public ProductRepositoryCreateException(string message)
            : base(message)
        { }

        public ProductRepositoryCreateException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
