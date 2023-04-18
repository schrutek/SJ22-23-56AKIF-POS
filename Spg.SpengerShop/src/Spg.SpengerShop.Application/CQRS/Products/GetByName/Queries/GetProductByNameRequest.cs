using MediatR;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries
{
    public class GetProductByNameRequest : IRequest<Product>
    {
        public string Name { get; set; }

        public GetProductByNameRequest(string name)
        {
            Name = name;
        }
    }
}
