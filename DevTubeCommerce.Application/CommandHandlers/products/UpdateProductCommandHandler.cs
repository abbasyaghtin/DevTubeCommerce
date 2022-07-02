using DevTubeCommerce.Application.Contract.Commands.Products;
using DevTubeCommerce.Domain.Core.Catalogs.Features;
using DevTubeCommerce.Domain.Core.Catalogs.Products;
using DevTubeCommerce.Domain.Core.Shared;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using DevTubeCommerce.Infrastructure.Patterns;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.CommandHandlers.products
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(new ProductId(request.Id), cancellationToken);
            if (product == null)
                throw new NotFoundException(Error.ProductNotFound);

            var oldProductFeatureValues = product.ProductFeatureValues
                .Select(x => new ProductFeatureValueData(x.FeatureId, x.Value)).ToList();
            var currentProductFeatureValues = request.ProductFeatures
                .Select(x => new ProductFeatureValueData(new FeatureId(x.FeatureId), x.Value)).ToList();

            product.Update(new ProductId(request.Id), request.Title, request.Description, request.Code, request.Price, oldProductFeatureValues, currentProductFeatureValues);
            await unitOfWork.SaveChangesAsync();
            return new UpdateProductCommandResponse();
        }
    }
}
