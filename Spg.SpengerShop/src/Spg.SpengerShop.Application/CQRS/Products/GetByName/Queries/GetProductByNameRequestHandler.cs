using MediatR;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.CQRS.Products.GetByName.Queries
{
    public class GetProductByNameRequestHandler : IRequestHandler<GetProductByNameRequest, ProductDto>
    {
        private readonly IReadOnlyRepositoryBase<Product> _repository;
        //private readonly SpengerShopContext _db;

        public GetProductByNameRequestHandler(IReadOnlyRepositoryBase<Product> repository)
        {
            _repository = repository;
        }

        public async Task<ProductDto> Handle(GetProductByNameRequest request, CancellationToken cancellationToken)
        {
            //List<Product> result = _db.Products.Include(p => p.ShoppingCartItems).ToList();

            return await Task.Run(() =>
            {
                Product result = _repository.GetByPKAndIncudes(request.Name, 
                    new List<System.Linq.Expressions.Expression<Func<Product, IEnumerable<ShoppingCartItem>>>?>() 
                    { 
                        p => p.ShoppingCartItems 
                    })
                    ?? throw new ProductNotFoundServiceException($"Produkt '{request.Name} 'wurde nicht gefunden!");

                ProductDto model = new ProductDto(
                    result.Name,
                    result.Tax,
                    result.Ean,
                    result.Material,
                    result.ExpiryDate)
                    {
                        ShoppingCartItems = result.ShoppingCartItems.Select(i => 
                            new ShoppingCartItemDto(i.Id, i is ShippableShoppingCartItem)
                        {
                            ProductNavigation = new ProductDto(
                                i.ProductNavigation.Name,
                                i.ProductNavigation.Tax,
                                i.ProductNavigation.Ean,
                                i.ProductNavigation.Material,
                                i.ProductNavigation.ExpiryDate)
                        }).ToList()
                    };
                return model;
            });
        }
    }
}
