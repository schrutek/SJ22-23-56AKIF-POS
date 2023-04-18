using MediatR;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.CQRS.Products.FilterByExpiryDate.Queries
{
    public class GetByExpiryDateRequestHandler : IRequestHandler<GetByExpiryDateRequest, IQueryable<Product>>
    {
        private readonly IReadOnlyRepositoryBase<Product> _readOnlyRepository;

        public GetByExpiryDateRequestHandler(IReadOnlyRepositoryBase<Product> readOnlyRepository)
        {
            _readOnlyRepository = readOnlyRepository;
        }

        public async Task<IQueryable<Product>> Handle(GetByExpiryDateRequest request, CancellationToken cancellationToken)
        {
            return await Task.Run(() => 
                _readOnlyRepository.Get(p => p.ExpiryDate > request.ExpiryDate));
        }
    }
}
