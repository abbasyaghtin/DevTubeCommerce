using DevTubeCommerce.Application.Contract.Commands.Products;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using DevTubeCommerce.Domain.Core.Catalogs.Products;
using DevTubeCommerce.Domain.Core.Shared;
using DevTubeCommerce.Infrastructure.Patterns;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.CommandHandlers.products
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductCommandResponse>
    {
        private readonly IProductRepository productRepository;
        private readonly IUnitOfWork unitOfWork;
        public AddProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<AddProductCommandResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var productFeatureValueData = request.ProductFeatures.Select(x => new
                                 ProductFeatureValueData(new FeatureId(x.FeatureId), x.Value)).ToList();

            var product = Product.CreateNew(request.Title, request.Description, request.Code, request.Price, productFeatureValueData);

            await productRepository.InsertAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync();

            return new AddProductCommandResponse { productId = product.Id.Value };
        }
    }
}
