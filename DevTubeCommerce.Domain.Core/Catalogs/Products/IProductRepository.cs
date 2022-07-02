using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Domain.Core.Catalogs.Products
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Product> GetByIdAsync(ProductId id, CancellationToken cancellationToken=default);
        Task<ProductId> InsertAsync(Product product, CancellationToken cancellationToken=default);
    }
}
