using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Application.Contract.Queries.Products
{
    public class GetAllProductsQuery : IRequest<List<GetProductQueryResponse>>
    {
    }
}
