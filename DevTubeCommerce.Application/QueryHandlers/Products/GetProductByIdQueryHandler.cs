using DevTubeCommerce.Application.Contract.Dto.Product;
using DevTubeCommerce.Application.Contract.Queries.Products;
using DevTubeCommerce.Domain.Core.Catalogs.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.QueryHandlers.Products
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductQueryResponse>
    {
        private readonly IProductRepository productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(new ProductId(request.Id), cancellationToken);
            var queryResponse = new GetProductQueryResponse()
            {
                Id = product.Id.Value,
                Code = product.Code,
                Description = product.Description,
                Price = product.Price,
                Title = product.Title,
            };
            queryResponse.ProductFeatures = product.ProductFeatureValues.Select(x => new ProductFeatureDto()
            {
                FeatureId = x.FeatureId.Value,
                Value = x.Value,
            }).ToList();
            return queryResponse;
        }
    }
}
