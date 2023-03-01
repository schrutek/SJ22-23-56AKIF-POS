using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Exceptions
{
    public class CreateProductServiceException : Exception
    {
        public CreateProductServiceException() 
            :base()
        { }

        public CreateProductServiceException(string message)
            : base(message)
        { }

        public CreateProductServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
