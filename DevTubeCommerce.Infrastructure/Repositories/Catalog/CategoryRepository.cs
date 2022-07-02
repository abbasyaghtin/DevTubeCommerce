using DevTubeCommerce.Domain.Core.Catalogs.Categories;
using DevTubeCommerce.Framework.Exceptions;
using DevTubeCommerce.Framework.Global;
using DevTubeCommerce.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTubeCommerce.Infrastructure.Repositories.Catalog
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CommerceContext context;

        public CategoryRepository(CommerceContext context)
        {
            this.context = context;
        }

        public async Task<Category> GetByIdAsync(CategoryId categoryId, CancellationToken cancellationToken)
        {
            return await context.Categories.FindAsync(new object[] { categoryId }, cancellationToken);
        }

        public async Task<List<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return await context.Categories.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<CategoryId> InsertCategoryAsync(Category category, CancellationToken cancellationToken = default)
        {
            await context.AddAsync(category);
            return category.Id;
        }
    }
}
