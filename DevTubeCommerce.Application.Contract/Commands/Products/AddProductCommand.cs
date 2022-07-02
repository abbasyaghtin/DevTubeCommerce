using DevTubeCommerce.Application.Contract.Dto.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Contract.Commands.Products
{
    public class AddProductCommand : IRequest<AddProductCommandResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public List<ProductFeatureDto> ProductFeatures { get; set; }
    }
    public class AddProductCommandResponse
    {
        public Guid productId { get; set; }
    }
}
