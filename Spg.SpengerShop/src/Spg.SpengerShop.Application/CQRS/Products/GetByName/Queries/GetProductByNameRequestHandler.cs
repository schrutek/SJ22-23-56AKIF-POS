using MediatR;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries
{
    public class GetProductByNameRequestHandler : IRequestHandler<GetProductByNameRequest, Product>
    {
        private readonly IReadOnlyRepositoryBase<Product> _repository;

        public GetProductByNameRequestHandler(IReadOnlyRepositoryBase<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Product> Handle(GetProductByNameRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => _repository.GetByPK(request.Name)
                ?? throw new ProductNotFoundServiceException($"Produkt '{request.Name} 'wurde nicht gefunden!"));
        }
    }
}
