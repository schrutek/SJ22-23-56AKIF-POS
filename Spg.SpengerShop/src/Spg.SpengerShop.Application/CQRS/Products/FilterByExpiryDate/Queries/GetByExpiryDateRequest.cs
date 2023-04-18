using MediatR;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.CQRS.Products.FilterByExpiryDate.Queries
{
    public class GetByExpiryDateRequest : IRequest<IQueryable<Product>>
    {
        public DateTime ExpiryDate { get; set; }

        public GetByExpiryDateRequest(DateTime expiryDate)
        {
            ExpiryDate = expiryDate;
        }
    }
}
