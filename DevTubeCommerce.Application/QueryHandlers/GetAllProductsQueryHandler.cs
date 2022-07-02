using DevTubeCommerce.Application.Contract.Dto.Product;
using DevTubeCommerce.Application.Contract.Queries.Products;
using DevTubeCommerce.Domain.Core.Catalogs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<GetProductQueryResponse>>
    {
        private readonly IProductRepository productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<GetProductQueryResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productRepository.GetAllAsync(cancellationToken);
           return products.Select(x => new GetProductQueryResponse()
            {
                Code = x.Code,
                Description = x.Description,
                Id = x.Id.Value,
                Price = x.Price,
                Title = x.Title,
                ProductFeatures = GetProductFeatures(x)

            }).ToList();
        }

        private static List<ProductFeatureDto> GetProductFeatures(Product product)
        {
            return product.ProductFeatureValues.Select(x => new ProductFeatureDto()
            {
                Value = x.Value,
                FeatureId = x.FeatureId.Value
            }).ToList();
        }
    }
}
