using DevTubeCommerce.Application.Contract.Dto.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Contract.Queries.Products
{
    public class GetProductByIdQuery : IRequest<GetProductQueryResponse>
    {
        public Guid Id { get; set; }
    }
    public class GetProductQueryResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public List<ProductFeatureDto> ProductFeatures { get; set; }
    }
}
