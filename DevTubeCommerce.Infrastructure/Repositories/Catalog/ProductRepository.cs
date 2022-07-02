using DevTubeCommerce.Domain.Core.Catalogs.Products;
using DevTubeCommerce.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Infrastructure.Repositories.Catalog
{
    public class ProductRepository : IProductRepository
    {
        private readonly CommerceContext context;

        public ProductRepository(CommerceContext context)
        {
            this.context = context;
        }
        public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken=default)
        {
            return await context.Products
                     .Include(x => x.ProductFeatureValues)
                     .ToListAsync(cancellationToken);
        }

        public async Task<Product> GetByIdAsync(ProductId id, CancellationToken cancellationToken=default)
        {
            return await context.Products
                      .Include(x => x.ProductFeatureValues)
                      .Where(x => x.Id == id)
                      .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ProductId> InsertAsync(Product product, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(product);
            return product.Id;
        }
    }
}
